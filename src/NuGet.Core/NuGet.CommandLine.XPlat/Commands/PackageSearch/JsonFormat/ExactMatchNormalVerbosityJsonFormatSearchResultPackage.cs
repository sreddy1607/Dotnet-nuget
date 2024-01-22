// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text.Json.Serialization;
using NuGet.Protocol.Core.Types;

namespace NuGet.CommandLine.XPlat
{
    internal class ExactMatchNormalVerbosityJsonFormatSearchResultPackage : ExactMatchMinimalVerbosityJsonFormatSearchResultPackage
    {
        [JsonPropertyName("totalDownloads")]
        public long? Downloads { get; set; }

        [JsonPropertyName("owners")]
        public string Owners { get; set; }

        public ExactMatchNormalVerbosityJsonFormatSearchResultPackage() : base() { }

        public ExactMatchNormalVerbosityJsonFormatSearchResultPackage(IPackageSearchMetadata packageSearchMetadata) : base(packageSearchMetadata)
        {
            Downloads = packageSearchMetadata.DownloadCount;
            Owners = packageSearchMetadata.Owners;
        }
    }
}
