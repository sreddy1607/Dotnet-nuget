// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text.Json.Serialization;
using NuGet.Protocol.Core.Types;

namespace NuGet.CommandLine.XPlat
{
    internal class ExactMatchMinimalVerbosityJsonFormatSearchResultPackage : ISearchResultPackage
    {
        [JsonPropertyName("id")]
        public string PackageId { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        public ExactMatchMinimalVerbosityJsonFormatSearchResultPackage() { }

        public ExactMatchMinimalVerbosityJsonFormatSearchResultPackage(IPackageSearchMetadata packageSearchMetadata)
        {
            PackageId = packageSearchMetadata.Identity.Id;
            Version = packageSearchMetadata.Identity.Version.ToString();
        }
    }
}
