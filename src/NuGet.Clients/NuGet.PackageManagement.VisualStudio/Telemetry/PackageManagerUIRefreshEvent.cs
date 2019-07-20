// All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
using System;
using NuGet.Common;

namespace NuGet.PackageManagement.Telemetry
{
    public class PackageManagerUIRefreshEvent : TelemetryEvent
    {
        private const string EventName = "PMUIRefreshOperation";

        public PackageManagerUIRefreshEvent(
        Guid parentId,
        bool isSolutionLevel,
        RefreshOperationSource refreshSource,
        RefreshOperationStatus refreshStatus,
        TimeSpan timeSinceLastRefresh) : base(EventName)
        {
            base["ParentId"] = parentId.ToString();
            base["IsSolutionLevel"] = isSolutionLevel;
            base["RefreshSource"] = refreshSource;
            base["RefreshStatus"] = refreshStatus;
            base["TimeSinceLastRefresh"] = timeSinceLastRefresh.TotalSeconds;
        }
    }

    public enum RefreshOperationSource
    {
        ProjectsChanged,
        CacheUpdated,
        ActionsExecuted,
        PackageManagerLoaded,
        PackageSourcesChanged,
        SourceSelectionChanged,
        FilterSelectionChanged,
        CheckboxPrereleaseChanged,
        ClearSearch,
        RestartSearchCommand,
    }

    public enum RefreshOperationStatus
    {
        Success,
        NotApplicable,
        NoOp,
    }
}
