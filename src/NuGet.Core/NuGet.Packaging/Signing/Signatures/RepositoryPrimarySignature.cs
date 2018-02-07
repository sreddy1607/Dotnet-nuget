// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
#if IS_DESKTOP
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
#endif
using NuGet.Common;

namespace NuGet.Packaging.Signing
{
    public class RepositoryPrimarySignature : PrimarySignature, IRepositorySignature
    {
#if IS_DESKTOP
        private readonly Uri _nuGetV3ServiceIndexUrl;
        public Uri NuGetV3ServiceIndexUrl => _nuGetV3ServiceIndexUrl;

        private readonly IReadOnlyList<string> _nuGetPackageOwners;
        public IReadOnlyList<string> NuGetPackageOwners => _nuGetPackageOwners;


        public RepositoryPrimarySignature(SignedCms signedCms)
            : base(signedCms, SignatureType.Repository)
        {
            _nuGetV3ServiceIndexUrl = AttributeUtility.GetNuGetV3ServiceIndexUrl(SignerInfo.SignedAttributes);
            _nuGetPackageOwners = AttributeUtility.GetNuGetPackageOwners(SignerInfo.SignedAttributes);
        }

        internal override SignatureVerificationStatus Verify(
            Timestamp timestamp,
            bool allowUntrusted,
            bool allowUntrustedSelfSignedCertificate,
            bool allowUnknownRevocation,
            HashAlgorithmName fingerprintAlgorithm,
            X509Certificate2Collection certificateExtraStore,
            List<SignatureLog> issues)
        {
            issues?.Add(SignatureLog.InformationLog(string.Format(CultureInfo.CurrentCulture, Strings.SignatureType, Type.ToString())));
            issues?.Add(SignatureLog.InformationLog(string.Format(CultureInfo.CurrentCulture, Strings.NuGetV3ServiceIndexUrl, _nuGetV3ServiceIndexUrl.ToString())));
            if (_nuGetPackageOwners != null)
            {
                issues?.Add(SignatureLog.InformationLog(string.Format(CultureInfo.CurrentCulture, Strings.NuGetPackageOwners, string.Join(", ", _nuGetPackageOwners))));
            }
            return base.Verify(timestamp, allowUntrusted, allowUntrustedSelfSignedCertificate, allowUnknownRevocation, fingerprintAlgorithm, certificateExtraStore, issues);
        }
#endif
    }
}
