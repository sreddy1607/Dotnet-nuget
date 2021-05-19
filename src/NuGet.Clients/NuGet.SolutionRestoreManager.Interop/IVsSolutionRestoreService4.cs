// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.SolutionRestoreManager
{
    /// <summary>
    /// Represents a package restore service API for integration with a project system.
    /// Implemented by NuGet.
    /// </summary>
    [ComImport]
    [Guid("72327117-6552-4685-BD7E-9C40A04B6EE5")]
    public interface IVsSolutionRestoreService4
    {
        /// <summary>
        /// A project system can call this service (optionally) to register itself to coordinate restore.
        /// Each project can only register once. NuGet will call into the source to wait for nominations for restore.
        /// NuGet will remove the registered object when a project is unloaded.
        /// </summary>
        /// <param name="restoreInfoSource">Represents a project specific info source</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        public Task RegisterRestoreInfoSourceAsync(IVsProjectRestoreInfoSource restoreInfoSource, CancellationToken cancellationToken);
    }
}
