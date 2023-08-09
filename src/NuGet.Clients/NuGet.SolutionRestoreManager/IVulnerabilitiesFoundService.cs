// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace NuGet.SolutionRestoreManager
{
    public interface IVulnerabilitiesFoundService
    {
        Task UpdateInfoBar(bool hasVulnerabilitiesInSolution, CancellationToken cancellationToken);
    }
}
