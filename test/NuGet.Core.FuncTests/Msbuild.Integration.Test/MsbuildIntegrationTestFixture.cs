// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using NuGet.XPlat.FuncTest;
using NuGet.Test.Utility;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Packaging.PackageExtraction;
using NuGet.Protocol;
using Xunit;

namespace Msbuild.Integration.Test
{
    public class MsbuildIntegrationTestFixture : IDisposable
    {
        private readonly TestDirectory _cliDirectory;
        private readonly string _dotnetCli = DotnetCliUtil.GetDotnetCli();
        internal readonly string _testDotnetCli;
        internal readonly string _msBuildSdksPath;
        internal readonly string _testDir;
        private readonly Dictionary<string, string> _processEnvVars = new Dictionary<string, string>();

        public MsbuildIntegrationTestFixture()
        {
            _cliDirectory = CopyLatestCliForPack();
            _testDotnetCli = Path.Combine(_cliDirectory, "dotnet.exe");
            _msBuildSdksPath = Path.Combine(Directory.GetDirectories
                (Path.Combine(_cliDirectory, "sdk"))
                .First(), "Sdks");
            _testDir = Directory.GetCurrentDirectory();
            _processEnvVars.Add("MSBuildSDKsPath", _msBuildSdksPath);
            _processEnvVars.Add("UseSharedCompilation", "false");
            _processEnvVars.Add("DOTNET_MULTILEVEL_LOOKUP", "0");
            _processEnvVars.Add("MSBUILDDISABLENODEREUSE ", "true");
            // We do this here so that dotnet new will extract all the packages on the first run on the machine.
            InitDotnetNewToExtractPackages();
        }

        private void InitDotnetNewToExtractPackages()
        {
            using (var testDirectory = TestDirectory.Create())
            {
                var projectName = "ClassLibrary1";
                CreateDotnetNewProject(testDirectory.Path, projectName, " classlib", timeOut: 300000);
            }

            using (var testDirectory = TestDirectory.Create())
            {
                var projectName = "ConsoleApp1";
                CreateDotnetNewProject(testDirectory.Path, projectName, " console", timeOut: 300000);
            }
        }

        internal void CreateDotnetNewProject(string solutionRoot, string projectName, string args = "console", int timeOut = 60000)
        {
            var workingDirectory = Path.Combine(solutionRoot, projectName);
            if (!Directory.Exists(workingDirectory))
            {
                Directory.CreateDirectory(workingDirectory);
            }
            var result = CommandRunner.Run(_testDotnetCli,
                workingDirectory,
                $"new {args}",
                waitForExit: true,
                timeOutInMilliseconds: timeOut,
                environmentVariables: _processEnvVars);

            Assert.True(result.Item1 == 0, $"Creating project failed with following log information :\n {result.AllOutput}");
            Assert.True(string.IsNullOrWhiteSpace(result.Item3), $"Creating project failed with following message in error stream :\n {result.AllOutput}");
        }

        /// <summary>
        /// dotnet.exe args
        /// </summary>
        internal CommandRunnerResult RunDotnet(string workingDirectory, string args, bool ignoreExitCode = false)
        {

            var result = CommandRunner.Run(_testDotnetCli,
                workingDirectory,
                args,
                waitForExit: true,
                environmentVariables: _processEnvVars);

            if (!ignoreExitCode)
            {
                Assert.True(result.ExitCode == 0, $"dotnet.exe {args} command failed with following log information :\n {result.AllOutput}");
            }

            return result;
        }

        /// <summary>
        /// msbuild.exe args
        /// </summary>
        internal CommandRunnerResult RunMsBuild(string workingDirectory, string args, bool ignoreExitCode = false)
        {

            var msBuildExe = Path.Combine(_testDir, "MSBuild.exe");
            var restoreDllPath = Path.Combine(_testDir, "NuGet.Build.Tasks.dll");
            var nugetRestoreTargetsPath = Path.Combine(_testDir, "NuGet.targets");
            // Uncomment to debug the msbuild call
            // _processEnvVars.Add("DEBUG_RESTORE_TASK", "true");
            var result = CommandRunner.Run(msBuildExe,
                workingDirectory,
                $"/p:NuGetRestoreTargets={nugetRestoreTargetsPath} /p:RestoreTaskAssemblyFile={restoreDllPath} /p:ImportNuGetBuildTasksPackTargetsFromSdk=\"true\" {args}",
                waitForExit: true,
                environmentVariables: _processEnvVars);

            if (!ignoreExitCode)
            {
                Assert.True(result.ExitCode == 0, $"msbuild.exe {args} command failed with following log information :\n {result.AllOutput}");
            }

            return result;
        }

        private CommandRunnerResult PackProjectOrSolution(string workingDirectory, string file, string args, string nuspecOutputPath, bool validateSuccess)
        {
            var result = CommandRunner.Run(_testDotnetCli,
                workingDirectory,
                $"pack {file} {args} /p:NuspecOutputPath={nuspecOutputPath}",
                waitForExit: true,
                environmentVariables: _processEnvVars);
            if (validateSuccess)
            {
                Assert.True(result.Item1 == 0, $"Pack failed with following log information :\n {result.AllOutput}");
                Assert.True(result.Item3 == "", $"Pack failed with following message in error stream :\n {result.AllOutput}");
            }
            return result;
        }

        private TestDirectory CopyLatestCliForPack()
        {
            var cliDirectory = TestDirectory.Create();
            CopyLatestCliToTestDirectory(cliDirectory);
            UpdateCliWithLatestNuGetAssemblies(cliDirectory);
            return cliDirectory;
        }

        private void CopyLatestCliToTestDirectory(string destinationDir)
        {
            var cliDir = Path.GetDirectoryName(_dotnetCli);

            //Create sub-directory structure in destination
            foreach (var directory in Directory.GetDirectories(cliDir, "*", SearchOption.AllDirectories))
            {
                var destDir = destinationDir + directory.Substring(cliDir.Length);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
            }

            var lastWriteTime = DateTime.Now.AddDays(-2);

            //Copy files recursively to destination directories
            foreach (var fileName in Directory.GetFiles(cliDir, "*", SearchOption.AllDirectories))
            {
                var destFileName = destinationDir + fileName.Substring(cliDir.Length);
                // TODO - figure out a better solution than this. Why are those long files getting copied here?
                try
                {
                    File.Copy(fileName, destFileName);
                    File.SetLastWriteTime(destFileName, lastWriteTime);
                }
                catch
                {

                }
            }
        }

        private void UpdateCliWithLatestNuGetAssemblies(string cliDirectory)
        {
            var nupkgsDirectory = DotnetCliUtil.GetNupkgDirectoryInRepo();
            var pathToPackNupkg = FindMostRecentNupkg(nupkgsDirectory, "NuGet.Build.Tasks.Pack");

            var nupkgsToCopy = new List<string> { "NuGet.Build.Tasks", "NuGet.Versioning", "NuGet.Protocol", "NuGet.ProjectModel", "NuGet.Packaging", "NuGet.LibraryModel", "NuGet.Frameworks", "NuGet.DependencyResolver.Core", "NuGet.Configuration", "NuGet.Common", "NuGet.Commands", "NuGet.CommandLine.XPlat", "NuGet.Credentials" };

            var pathToSdkInCli = Path.Combine(
                    Directory.GetDirectories(Path.Combine(cliDirectory, "sdk"))
                        .First());
            using (var nupkg = new PackageArchiveReader(pathToPackNupkg))
            {
                var pathToPackSdk = Path.Combine(pathToSdkInCli, "Sdks", "NuGet.Build.Tasks.Pack");
                var files = nupkg.GetFiles()
                .Where(fileName => fileName.StartsWith("Desktop")
                                || fileName.StartsWith("CoreCLR")
                                || fileName.StartsWith("build")
                                || fileName.StartsWith("buildCrossTargeting"));

                DeleteDirectory(pathToPackSdk);
                CopyNupkgFilesToTarget(nupkg, pathToPackSdk, files);
            }


            foreach (var nupkgName in nupkgsToCopy)
            {
                using (var nupkg = new PackageArchiveReader(FindMostRecentNupkg(nupkgsDirectory, nupkgName)))
                {
                    var files = nupkg.GetFiles()
                    .Where(fileName => fileName.StartsWith("lib/netstandard")
                                    || fileName.StartsWith("lib/netcoreapp")
                                    || fileName.Contains("NuGet.targets"));

                    CopyFlatlistOfFilesToTarget(nupkg, pathToSdkInCli, files);
                }
            }
        }

        private void CopyFlatlistOfFilesToTarget(PackageArchiveReader nupkg, string destination, IEnumerable<string> packageFiles)
        {
            var packageFileExtractor = new PackageFileExtractor(packageFiles,
                             PackageExtractionBehavior.XmlDocFileSaveMode);
            var logger = new TestCommandOutputLogger();
            var token = CancellationToken.None;
            var filesCopied = new List<string>();

            foreach (var packageFile in packageFiles)
            {
                token.ThrowIfCancellationRequested();

                var entry = nupkg.GetEntry(packageFile);

                var packageFileName = entry.FullName;
                // An entry in a ZipArchive could start with a '/' based on how it is zipped
                // Remove it if present
                if (packageFileName.StartsWith("/", StringComparison.Ordinal))
                {
                    packageFileName = packageFileName.Substring(1);
                }
                // Get only the name, without the path, since we are extracting to flat list
                packageFileName = Path.GetFileName(packageFileName);

                // ZipArchive always has forward slashes in them. By replacing them with DirectorySeparatorChar;
                // in windows, we get the windows-style path
                var normalizedPath = Uri.UnescapeDataString(packageFileName.Replace('/', Path.DirectorySeparatorChar));

                var targetFilePath = Path.Combine(destination, normalizedPath);
                if (!targetFilePath.StartsWith(destination, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                try
                {
                    File.Delete(targetFilePath);
                }
                catch
                {
                    // Do nothing
                }
                using (var stream = entry.Open())
                {
                    var copiedFile = packageFileExtractor.ExtractPackageFile(packageFileName, targetFilePath, stream);
                    if (copiedFile != null)
                    {
                        entry.UpdateFileTimeFromEntry(copiedFile, logger);

                        filesCopied.Add(copiedFile);
                    }
                }
            }

        }

        private void CopyNupkgFilesToTarget(PackageArchiveReader nupkg, string destPath, IEnumerable<string> files)
        {
            var packageFileExtractor = new PackageFileExtractor(files,
                                         PackageExtractionBehavior.XmlDocFileSaveMode);

            nupkg.CopyFiles(destPath, files, packageFileExtractor.ExtractPackageFile, new TestCommandOutputLogger(),
                CancellationToken.None);

        }

        private static string FindMostRecentNupkg(string nupkgDirectory, string id)
        {
            var info = LocalFolderUtility.GetPackagesV2(nupkgDirectory, new TestLogger());

            return info.Where(t => t.Identity.Id.Equals(id, StringComparison.OrdinalIgnoreCase))
                .Where(t => !Path.GetExtension(Path.GetFileNameWithoutExtension(t.Path)).Equals(".symbols"))
                .OrderByDescending(p => p.LastWriteTimeUtc)
                .First().Path;
        }

        public void Dispose()
        {
            RunDotnet(Path.GetDirectoryName(_testDotnetCli), "build-server shutdown");
            KillDotnetExe(_testDotnetCli);
            _cliDirectory.Dispose();
        }

        private static void KillDotnetExe(string pathToDotnetExe)
        {
            var processes = Process.GetProcessesByName("dotnet")
                .Where(t => string.Compare(t.MainModule.FileName, Path.GetFullPath(pathToDotnetExe), ignoreCase: true) == 0);
            var testDirProcesses = Process.GetProcesses()
                .Where(t => t.MainModule.FileName.StartsWith(TestFileSystemUtility.NuGetTestFolder, StringComparison.OrdinalIgnoreCase));
            try
            {
                if (processes != null)
                {
                    foreach (var process in processes)
                    {
                        if (string.Compare(process.MainModule.FileName, Path.GetFullPath(pathToDotnetExe), true) == 0)
                        {
                            process.Kill();
                        }
                    }
                }

                if (testDirProcesses != null)
                {
                    foreach (var process in testDirProcesses)
                    {
                        process.Kill();
                    }
                }

            }
            catch { }
        }

        /// <summary>
        /// Depth-first recursive delete, with handling for descendant 
        /// directories open in Windows Explorer or used by another process
        /// </summary>
        private static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
            catch
            {

            }
        }
    }
}
