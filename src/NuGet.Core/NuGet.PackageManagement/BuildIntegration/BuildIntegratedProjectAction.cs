﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NuGet.Commands;
using NuGet.Frameworks;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using NuGet.ProjectModel;
using NuGet.Protocol.Core.Types;

namespace NuGet.PackageManagement
{
    public class BuildIntegratedProjectAction : NuGetProjectAction
    {
        /// <summary>
        /// Before the update
        /// </summary>
        public LockFile OriginalLockFile { get; }

        /// <summary>
        /// After applying the changes
        /// </summary>
        public RestoreResult RestoreResult { get; }

        /// <summary>
        /// After applying the changes
        /// </summary>
        public RestoreResultPair RestoreResultPair { get; }

        /// <summary>
        /// Sources used for package restore.
        /// </summary>
        public IReadOnlyList<SourceRepository> Sources { get; }

        /// <summary>
        /// Original user actions.
        /// </summary>
        public IReadOnlyList<NuGetProjectAction> OriginalActions { get; }

        /// <summary>
        /// Shows the frameworks for which a preview restore operation was successful. Only use it
        /// in case of single package install case, and only for CpsPackageReference projects.
        /// </summary>
        public IEnumerable<NuGetFramework> SuccessfulFrameworksForPreviewRestore { get; }

        /// <summary>
        /// Shows the frameworks for which a preview restore operation was unsuccessful. Only use it
        /// in case of single package install case, and only for CpsPackageReference projects.
        /// </summary>
        public IEnumerable<NuGetFramework> UnsuccessfulFrameworksForPreviewRestore { get; }

        public BuildIntegratedProjectAction(NuGetProject project,
            PackageIdentity packageIdentity,
            NuGetProjectActionType nuGetProjectActionType,
            LockFile originalLockFile,
            RestoreResultPair restoreResultPair,
            IReadOnlyList<SourceRepository> sources,
            IReadOnlyList<NuGetProjectAction> originalActions,
            IEnumerable<NuGetFramework> successfulFrameworks,
            IEnumerable<NuGetFramework> unsuccessfulFrameworks)
            : base(packageIdentity, nuGetProjectActionType, project)
        {
            if (packageIdentity == null)
            {
                throw new ArgumentNullException(nameof(packageIdentity));
            }

            if (originalLockFile == null)
            {
                throw new ArgumentNullException(nameof(originalLockFile));
            }

            if (restoreResultPair == null)
            {
                throw new ArgumentNullException(nameof(restoreResultPair));
            }

            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (originalActions == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (successfulFrameworks == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (unsuccessfulFrameworks == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            OriginalLockFile = originalLockFile;
            RestoreResult = restoreResultPair.Result;
            RestoreResultPair = restoreResultPair;
            Sources = sources;
            OriginalActions = originalActions;
            SuccessfulFrameworksForPreviewRestore = successfulFrameworks;
            UnsuccessfulFrameworksForPreviewRestore = unsuccessfulFrameworks;
        }

        public IReadOnlyList<NuGetProjectAction> GetProjectActions()
        {
            var actions = new List<NuGetProjectAction>();

            if (RestoreResult.Success)
            {
                var added = BuildIntegratedRestoreUtility.GetAddedPackages(OriginalLockFile, RestoreResult.LockFile);
                var removed = BuildIntegratedRestoreUtility.GetAddedPackages(RestoreResult.LockFile, OriginalLockFile);

                foreach (var package in removed)
                {
                    actions.Add(NuGetProjectAction.CreateUninstallProjectAction(package, Project));
                }

                foreach (var package in added)
                {
                    actions.Add(NuGetProjectAction.CreateInstallProjectAction(package, sourceRepository: null, project: Project));
                }
            }

            return actions;
        }
    }
}
