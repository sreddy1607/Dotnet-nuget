// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;

namespace NuGet.Packaging.Signing
{
    public class IntegrityVerificationProvider : ISignatureVerificationProvider
    {
        public Task<PackageVerificationResult> GetTrustResultAsync(ISignedPackageReader package, Signature signature, SignedPackageVerifierSettings settings, CancellationToken token)
        {
            return VerifyPackageIntegrityAsync(package, signature, settings);
        }

#if IS_DESKTOP
        private async Task<PackageVerificationResult> VerifyPackageIntegrityAsync(ISignedPackageReader package, Signature signature, SignedPackageVerifierSettings settings)
        {
            var status = SignatureVerificationStatus.Untrusted;
            var issues = new List<SignatureLog>();

            var validHashOids = SigningSpecifications.V1.AllowedHashAlgorithmOids;
            var signatureHashOid = signature.SignatureContent.HashAlgorithm.ConvertToOidString();
            if (validHashOids.Contains(signatureHashOid, StringComparer.InvariantCultureIgnoreCase))
            {
                issues.Add(SignatureLog.InformationLog(string.Format(CultureInfo.CurrentCulture, Strings.SignatureHashAlgorithm, signature.SignatureContent.HashAlgorithm)));

                try
                {
                    await package.ValidateIntegrityAsync(signature.SignatureContent, CancellationToken.None);
                    status = SignatureVerificationStatus.Trusted;
                }
                catch (Exception e)
                {
                    status = SignatureVerificationStatus.Invalid;
                    issues.Add(SignatureLog.Issue(true, NuGetLogCode.NU3015, Strings.SignaturePackageIntegrityFailure));
                    issues.Add(SignatureLog.DebugLog(e.ToString()));
                }
            }
            else
            {
                var code = !settings.AllowUntrusted ? NuGetLogCode.NU3013 : NuGetLogCode.NU3513;
                issues.Add(SignatureLog.Issue(!settings.AllowUntrusted, code, Strings.SignatureFailureInvalidHashAlgorithmOid));
                issues.Add(SignatureLog.DebugLog(string.Format(CultureInfo.CurrentCulture, Strings.SignatureDebug_HashOidFound, signatureHashOid)));
            }

            return new SignedPackageVerificationResult(status, signature, issues);
        }
#else
        private Task<PackageVerificationResult> VerifyPackageIntegrityAsync(ISignedPackageReader package, Signature signature, SignedPackageVerifierSettings settings)
        {
            throw new NotSupportedException();
        }
#endif
    }
}
