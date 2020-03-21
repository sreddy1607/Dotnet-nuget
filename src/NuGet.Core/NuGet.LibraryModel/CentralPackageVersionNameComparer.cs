// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace NuGet.LibraryModel
{
    public sealed class CentralPackageVersionNameComparer : IEqualityComparer<CentralPackageVersion>
    {
        private static readonly Lazy<CentralPackageVersionNameComparer> _defaultComparer = new Lazy<CentralPackageVersionNameComparer>(() => new CentralPackageVersionNameComparer());

        public static CentralPackageVersionNameComparer Default
        {
            get
            {
                return _defaultComparer.Value;
            }
        }

        private CentralPackageVersionNameComparer()
        {
        }

        public bool Equals(CentralPackageVersion x, CentralPackageVersion y)
        {
            return string.Equals(x?.Name, y?.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(CentralPackageVersion obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            return StringComparer.OrdinalIgnoreCase.GetHashCode(obj.Name);
        }
    }
}

