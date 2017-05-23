﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Frameworks;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using NuGet.ProjectManagement.Projects;
using NuGet.ProjectModel;
using NuGet.VisualStudio;
using EnvDTEProject = EnvDTE.Project;

namespace NuGet.PackageManagement.VisualStudio
{
    /// <summary>
    /// A nuget aware project system containing a .json file instead of a packages.config file
    /// </summary>
    public class ProjectJsonBuildIntegratedProjectSystem : ProjectJsonBuildIntegratedNuGetProject
    {
        private readonly EnvDTEProject _envDTEProject;
        private IScriptExecutor _scriptExecutor;

        public ProjectJsonBuildIntegratedProjectSystem(
            string jsonConfigPath,
            string msbuildProjectFilePath,
            EnvDTEProject envDTEProject,
            string uniqueName)
            : base(jsonConfigPath, msbuildProjectFilePath)
        {
            _envDTEProject = envDTEProject;

            // set project id
            var projectId = VsHierarchyUtility.GetProjectId(envDTEProject);
            InternalMetadata.Add(NuGetProjectMetadataKeys.ProjectId, projectId);
            InternalMetadata.Add(NuGetProjectMetadataKeys.UniqueName, uniqueName);
        }

        protected override async Task UpdateInternalTargetFramework()
        {
            // VsHierarchyUtility.ToVsHierarchy can only be run on UI Thread.
            await NuGetUIThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            // Override the JSON TFM value from the csproj for UAP framework
            if (InternalMetadata.TryGetValue(NuGetProjectMetadataKeys.TargetFramework, out object targetFramework))
            {
                var jsonTargetFramework = targetFramework as NuGetFramework;
                if (IsUAPFramework(jsonTargetFramework))
                {
                    var platformMinVersionString = VsHierarchyUtility.GetMSBuildProperty(
                        VsHierarchyUtility.ToVsHierarchy(_envDTEProject),
                        EnvDTEProjectInfoUtility.TargetPlatformMinVersion);

                    var platformMinVersion = !string.IsNullOrEmpty(platformMinVersionString)
                        ? new Version(platformMinVersionString)
                        : null;

                    if (platformMinVersion != null && jsonTargetFramework.Version != platformMinVersion)
                    {
                        // Found the TPMinV in csproj and it is different from project json's framework version, 
                        // store this as a new target framework to be replaced in project.json
                        var newTargetFramework = new NuGetFramework(jsonTargetFramework.Framework, platformMinVersion);
                        InternalMetadata[NuGetProjectMetadataKeys.TargetFramework] = newTargetFramework;
                    }
                }
            }
        }

        private IScriptExecutor ScriptExecutor
        {
            get
            {
                if (_scriptExecutor == null)
                {
                    _scriptExecutor = ServiceLocator.GetInstanceSafe<IScriptExecutor>();
                }

                return _scriptExecutor;
            }
        }

        public override async Task<bool> ExecuteInitScriptAsync(
            PackageIdentity identity,
            string packageInstallPath,
            INuGetProjectContext projectContext,
            bool throwOnFailure)
        {
            return
                await
                    ScriptExecutorUtil.ExecuteScriptAsync(identity, packageInstallPath, projectContext, ScriptExecutor,
                        _envDTEProject, throwOnFailure);
        }

        public override Task<IReadOnlyList<ProjectRestoreReference>> GetDirectProjectReferencesAsync(DependencyGraphCacheContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var resolvedProjects = context.DeferredPackageSpecs.Select(project => project.Name);

            return VSProjectRestoreReferenceUtility.GetDirectProjectReferences(_envDTEProject, resolvedProjects, context.Logger);
        }
    }
}
