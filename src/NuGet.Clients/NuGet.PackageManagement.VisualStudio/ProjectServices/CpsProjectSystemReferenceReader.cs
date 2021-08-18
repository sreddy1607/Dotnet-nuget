// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.VisualStudio.ProjectSystem.Properties;
using Microsoft.VisualStudio.ProjectSystem.References;
using Microsoft.VisualStudio.Threading;
using NuGet.Frameworks;
using NuGet.LibraryModel;
using NuGet.ProjectManagement;
using NuGet.ProjectModel;
using NuGet.VisualStudio;

namespace NuGet.PackageManagement.VisualStudio
{
    /// <summary>
    /// Reference reader implementation for the core project system in the integrated development environment (IDE).
    /// </summary>
    internal class CpsProjectSystemReferenceReader
        : IProjectSystemReferencesReader
    {
        private readonly IVsProjectAdapter _vsProjectAdapter;
        private readonly IVsProjectThreadingService _threadingService;
        private readonly AsyncLazy<ConfiguredProject> _configuredProject;

        public CpsProjectSystemReferenceReader(
            IVsProjectAdapter vsProjectAdapter,
            IVsProjectThreadingService threadingService)
        {
            Assumes.Present(vsProjectAdapter);
            Assumes.Present(threadingService);

            _vsProjectAdapter = vsProjectAdapter;
            _threadingService = threadingService;
            _configuredProject = new AsyncLazy<ConfiguredProject>(async () =>
            {
                await _threadingService.JoinableTaskFactory.SwitchToMainThreadAsync();

                var context = _vsProjectAdapter.Project as IVsBrowseObjectContext;
                if (context == null)
                {
                    // VC implements this on their DTE.Project.Object
                    context = _vsProjectAdapter.Project.Object as IVsBrowseObjectContext;
                }
                return context?.ConfiguredProject;
            }, threadingService.JoinableTaskFactory);
        }

        public async Task<IEnumerable<ProjectRestoreReference>> GetProjectReferencesAsync(
            Common.ILogger logger, CancellationToken _)
        {
            var project = await _configuredProject.GetValueAsync();
            IBuildDependencyProjectReferencesService service = project.Services.ProjectReferences;

            if (service == null)
            {
                return Enumerable.Empty<ProjectRestoreReference>();
            }
            var results = new List<ProjectRestoreReference>();
            var hasMissingReferences = false;

            foreach (IUnresolvedBuildDependencyProjectReference projectReference in await service.GetUnresolvedReferencesAsync())
            {
                try
                {
                    if (await projectReference.GetReferenceOutputAssemblyAsync())
                    {
                        string childProjectPath = projectReference.EvaluatedIncludeAsFullPath;
                        var projectRestoreReference = new ProjectRestoreReference()
                        {
                            ProjectPath = childProjectPath,
                            ProjectUniqueName = childProjectPath
                        };

                        results.Add(projectRestoreReference);
                    }
                }
                catch (Exception ex)
                {
                    hasMissingReferences = true;
                    logger.LogDebug(ex.ToString());
                }
            }

            if (hasMissingReferences)
            {
                // Log a generic message once per project if any items could not be resolved.
                // In most cases this can be ignored, but in the rare case where the unresolved
                // item is actually a project the restore result will be incomplete.
                var message = string.Format(
                    CultureInfo.CurrentCulture,
                    Strings.UnresolvedItemDuringProjectClosureWalk,
                    _vsProjectAdapter.UniqueName);

                logger.LogVerbose(message);
            }

            return results;
        }

        public Task<IEnumerable<LibraryDependency>> GetPackageReferencesAsync(
            NuGetFramework targetFramework, CancellationToken _)
        {
            throw new NotSupportedException();
        }
    }
}
