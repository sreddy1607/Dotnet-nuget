// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NuGet.Common;
using NuGet.Frameworks;
using NuGet.LibraryModel;
using NuGet.ProjectModel.ProjectLockFile;
using NuGet.Shared;

namespace NuGet.ProjectModel
{
    public static class PackagesLockFileUtilities
    {
        public static bool IsNuGetLockFileEnabled(PackageSpec project)
        {
            var restorePackagesWithLockFile = project.RestoreMetadata?.RestoreLockProperties.RestorePackagesWithLockFile;
            return MSBuildStringUtility.IsTrue(restorePackagesWithLockFile) || File.Exists(GetNuGetLockFilePath(project));
        }

        public static string GetNuGetLockFilePath(PackageSpec project)
        {
            if (project.RestoreMetadata == null || project.BaseDirectory == null)
            {
                // RestoreMetadata or project BaseDirectory is not set which means it's probably called through test.
                return null;
            }

            var path = project.RestoreMetadata.RestoreLockProperties.NuGetLockFilePath;

            if (!string.IsNullOrEmpty(path))
            {
                return Path.Combine(project.BaseDirectory, path);
            }

            var projectName = Path.GetFileNameWithoutExtension(project.RestoreMetadata.ProjectPath);
            return GetNuGetLockFilePath(project.BaseDirectory, projectName);
        }

        public static string GetNuGetLockFilePath(string baseDirectory, string projectName)
        {
            if (!string.IsNullOrEmpty(projectName))
            {
                var path = Path.Combine(baseDirectory, "packages." + projectName.Replace(' ', '_') + ".lock.json");

                if (File.Exists(path))
                {
                    return path;
                }
            }

            return Path.Combine(baseDirectory, PackagesLockFileFormat.LockFileName);
        }

        public static bool IsLockFileStillValid(DependencyGraphSpec dgSpec, PackagesLockFile nuGetLockFile)
        {
            var uniqueName = dgSpec.Restore.First();
            var project = dgSpec.GetProjectSpec(uniqueName);

            // Validate all the direct dependencies
            foreach (var framework in project.TargetFrameworks)
            {
                var target = nuGetLockFile.Targets.FirstOrDefault(
                    t => EqualityUtility.EqualsWithNullCheck(t.TargetFramework, framework.FrameworkName));

                if (target == null)
                {
                    // a new target found in the dgSpec so invalidate existing lock file.
                    return false;
                }

                var directDependencies = target.Dependencies.Where(dep => dep.Type == PackageDependencyType.Direct);

                if (HasProjectDependencyChanged(framework.Dependencies, directDependencies))
                {
                    // lock file is out of sync
                    return false;
                }
            }

            // Validate all P2P references
            foreach (var framework in project.RestoreMetadata.TargetFrameworks)
            {
                var target = nuGetLockFile.Targets.FirstOrDefault(
                    t => EqualityUtility.EqualsWithNullCheck(t.TargetFramework, framework.FrameworkName));

                if (target == null)
                {
                    // a new target found in the dgSpec so invalidate existing lock file.
                    return false;
                }

                var queue = new Queue<Tuple<string, string>>();
                var visitedP2PReference = new HashSet<string>();

                foreach (var projectReference in framework.ProjectReferences)
                {
                    if (visitedP2PReference.Add(projectReference.ProjectUniqueName))
                    {
                        var spec = dgSpec.GetProjectSpec(projectReference.ProjectUniqueName);
                        queue.Enqueue(new Tuple<string, string>(spec.Name, projectReference.ProjectUniqueName));

                        while (queue.Count > 0)
                        {
                            var projectNames = queue.Dequeue();
                            var p2pUniqueName = projectNames.Item2;
                            var p2pProjectName = projectNames.Item1;

                            var projectDependency = target.Dependencies.FirstOrDefault(
                                dep => dep.Type == PackageDependencyType.Project &&
                                StringComparer.OrdinalIgnoreCase.Equals(dep.Id, p2pProjectName));

                            if (projectDependency == null)
                            {
                                // project dependency doesn't exist in lock file.
                                return false;
                            }

                            var p2pSpec = dgSpec.GetProjectSpec(p2pUniqueName);

                            // The package spec not found in the dg spec. This could mean that the project does not exist anymore.
                            if (p2pSpec != null)
                            {
                                // This does not consider ATF.
                                var p2pSpecTargetFrameworkInformation = NuGetFrameworkUtility.GetNearest(p2pSpec.TargetFrameworks, framework.FrameworkName, e => e.FrameworkName);

                                // No compatible framework found
                                if (p2pSpecTargetFrameworkInformation != null)
                                {
                                    // We need to compare the main framework only. Ignoring fallbacks.
                                    var p2pSpecProjectRestoreMetadataFrameworkInfo = p2pSpec.RestoreMetadata.TargetFrameworks.FirstOrDefault(
                                        t => NuGetFramework.Comparer.Equals(p2pSpecTargetFrameworkInformation.FrameworkName, t.FrameworkName));

                                    if (p2pSpecProjectRestoreMetadataFrameworkInfo != null)
                                    {
                                        if (HasP2PDependencyChanged(p2pSpecTargetFrameworkInformation.Dependencies, p2pSpecProjectRestoreMetadataFrameworkInfo.ProjectReferences, projectDependency, dgSpec))
                                        {
                                            // P2P transitive package dependencies have changed
                                            return false;
                                        }

                                        foreach (var reference in p2pSpecProjectRestoreMetadataFrameworkInfo.ProjectReferences)
                                        {
                                            if (visitedP2PReference.Add(reference.ProjectUniqueName))
                                            {
                                                var referenceSpec = dgSpec.GetProjectSpec(reference.ProjectUniqueName);
                                                queue.Enqueue(new Tuple<string, string>(referenceSpec.Name, reference.ProjectUniqueName));
                                            }
                                        }
                                    }
                                    else // This should never happen.
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>Compares two lock files to check if the structure is the same (all values are the same, other
        /// than SHA hash), and matches dependencies so the caller can easily compare SHA hashes.</summary>
        /// <param name="expected">The expected lock file structure. Usuaully generated from the project.</param>
        /// <param name="actual">The lock file that was loaded from the file on disk.</param>
        /// <returns>A ValueTuple containing two fields. First, a boolean to represent whether the lock file is still
        /// valid (structure of the two lock files are the same). Second, a list of matched dependencies if the lock
        /// file is still valid, otherwise null.</returns>
        public static (bool lockFileStillValid, List<KeyValuePair<LockFileDependency, LockFileDependency>> matchedDependencies)
            IsLockFileStillValid(PackagesLockFile expected, PackagesLockFile actual)
        {
            // do quick checks for obvious structure differences
            if (expected.Version != actual.Version)
            {
                return (false, null);
            }

            if (expected.Targets.Count != actual.Targets.Count)
            {
                return (false, null);
            }

            foreach (var expectedTarget in expected.Targets)
            {
                PackagesLockFileTarget actualTarget = null;

                for (var i = 0; i < actual.Targets.Count; i++)
                {
                    if (actual.Targets[i].TargetFramework == expectedTarget.TargetFramework)
                    {
                        if (actualTarget == null)
                        {
                            actualTarget = actual.Targets[i];
                        }
                        else
                        {
                            // more than 1? possible bug or bad hand edited lock file.
                            return (false, null);
                        }
                    }

                    if (actualTarget == null)
                    {
                        return (false, null);
                    }

                    if (actualTarget.Dependencies.Count != expectedTarget.Dependencies.Count)
                    {
                        return (false, null);
                    }
                }
            }

            // no obvious structure difference, so start trying to match individual dependencies
            var matchedDependencies = new List<KeyValuePair<LockFileDependency, LockFileDependency>>();
            var lockFileStillValid = true;
            var dependencyComparer = LockFileDependencyComparerWithoutContentHash.Default;

            foreach (var expectedTarget in expected.Targets)
            {
                var actualTarget = actual.Targets.Single(t => t.TargetFramework == expectedTarget.TargetFramework);

                // Duplicate dependencies list so we can remove matches to validate that all dependencies were matched
                var actualDependencies = new Dictionary<LockFileDependency, LockFileDependency>(
                    actualTarget.Dependencies.Count, 
                    dependencyComparer
                    );
                foreach (var actualDependency in actualTarget.Dependencies)
                {
                    actualDependencies.Add(actualDependency, actualDependency);
                }

                foreach (var expectedDependency in expectedTarget.Dependencies)
                {
                    if (actualDependencies.TryGetValue(expectedDependency, out var actualDependency))
                    {
                        matchedDependencies.Add(new KeyValuePair<LockFileDependency, LockFileDependency>(expectedDependency, actualDependency));
                        actualDependencies.Remove(actualDependency);
                    }
                    else
                    {
                        return (false, null);
                    }
                }

                if (actualDependencies.Count != 0)
                {
                    return (false, null);
                }
            }

            return (lockFileStillValid, matchedDependencies);
        }

        private static bool HasProjectDependencyChanged(IEnumerable<LibraryDependency> newDependencies, IEnumerable<LockFileDependency> lockFileDependencies)
        {
            // If the count is not the same, something has changed.
            // Otherwise we N^2 walk below determines whether anything has changed.
            var newPackageDependencies = newDependencies.Where(dep => dep.LibraryRange.TypeConstraint == LibraryDependencyTarget.Package);
            if(newPackageDependencies.Count() != lockFileDependencies.Count())
            {
                return true;
            }

            foreach (var dependency in newPackageDependencies)
            {
                var lockFileDependency = lockFileDependencies.FirstOrDefault(d => StringComparer.OrdinalIgnoreCase.Equals(d.Id, dependency.Name));

                if (lockFileDependency == null || !EqualityUtility.EqualsWithNullCheck(lockFileDependency.RequestedVersion, dependency.LibraryRange.VersionRange))
                {
                    // dependency has changed and lock file is out of sync.
                    return true;
                }
            }

            // no dependency changed. Lock file is still valid.
            return false;
        }

        private static bool HasP2PDependencyChanged(IEnumerable<LibraryDependency> newDependencies, IEnumerable<ProjectRestoreReference> projectRestoreReferences, LockFileDependency projectDependency, DependencyGraphSpec dgSpec)
        {
            if (projectDependency == null)
            {
                // project dependency doesn't exists in lock file so it's out of sync.
                return true;
            }

            // If the count is not the same, something has changed.
            // Otherwise we N^2 walk below determines whether anything has changed.
            var transitivelyFlowingDependencies = newDependencies.Where(
                dep => (dep.LibraryRange.TypeConstraint == LibraryDependencyTarget.Package && dep.SuppressParent != LibraryIncludeFlags.All));

            if (transitivelyFlowingDependencies.Count() + projectRestoreReferences.Count() != projectDependency.Dependencies.Count)
            {
                return true;
            }

            foreach (var dependency in transitivelyFlowingDependencies)
            {
                var matchedP2PLibrary = projectDependency.Dependencies.FirstOrDefault(dep => StringComparer.OrdinalIgnoreCase.Equals(dep.Id, dependency.Name));

                if (matchedP2PLibrary == null || !EqualityUtility.EqualsWithNullCheck(matchedP2PLibrary.VersionRange, dependency.LibraryRange.VersionRange))
                {
                    // P2P dependency has changed and lock file is out of sync.
                    return true;
                }
            }

            foreach (var dependency in projectRestoreReferences)
            {
                var referenceSpec = dgSpec.GetProjectSpec(dependency.ProjectUniqueName);
                var matchedP2PLibrary = projectDependency.Dependencies.FirstOrDefault(dep => StringComparer.OrdinalIgnoreCase.Equals(dep.Id, referenceSpec.Name));

                if (matchedP2PLibrary == null) // Do not check the version for the projects, or else https://github.com/nuget/home/issues/7935
                {
                    // P2P dependency has changed and lock file is out of sync.
                    return true;
                }
            }

            // no dependency changed. Lock file is still valid.
            return false;
        }

    }
}
