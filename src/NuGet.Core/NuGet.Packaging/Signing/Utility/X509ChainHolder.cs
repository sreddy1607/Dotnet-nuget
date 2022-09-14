// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Cryptography.X509Certificates;
using NuGet.Common;

namespace NuGet.Packaging.Signing
{
    /// <summary>
    /// Creates and performs cleanup on an <see cref="X509Chain" /> instance.
    /// </summary>
    /// <remarks>
    /// Certificates held by individual X509ChainElement objects should be disposed immediately after use to minimize
    /// finalizer impact.
    /// </remarks>
    public sealed class X509ChainHolder : IDisposable
    {
        private bool _isDisposed;

        public X509Chain Chain { get; }

        public X509ChainHolder()
        {
            IX509ChainFactory creator = X509TrustStore.GetX509ChainFactory(X509StorePurpose.CodeSigning, NullLogger.Instance);

            Chain = creator.Create();
        }

        private X509ChainHolder(X509StorePurpose storePurpose)
        {
            IX509ChainFactory creator = X509TrustStore.GetX509ChainFactory(storePurpose, NullLogger.Instance);

            Chain = creator.Create();
        }

        internal static X509ChainHolder CreateForCodeSigning()
        {
            return new X509ChainHolder(X509StorePurpose.CodeSigning);
        }

        internal static X509ChainHolder CreateForTimestamping()
        {
            return new X509ChainHolder(X509StorePurpose.Timestamping);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                foreach (var chainElement in Chain.ChainElements)
                {
                    chainElement.Certificate.Dispose();
                }

                Chain.Dispose();

                GC.SuppressFinalize(this);

                _isDisposed = true;
            }
        }
    }
}
