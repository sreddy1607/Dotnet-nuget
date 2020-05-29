// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol.Core.Types;

namespace NuGet.Protocol
{
    public class PackageSearchResourceV3 : PackageSearchResource
    {
        private readonly RawSearchResourceV3 _rawSearchResource;

        public PackageSearchResourceV3(RawSearchResourceV3 searchResource)
            : base()
        {
            _rawSearchResource = searchResource;
        }

        /// <summary>
        /// Query nuget package list from nuget server. This implementation optimized for performance so doesn't iterate whole result 
        /// returned nuget server, so as soon as find "take" number of result packages then stop processing and return the result. 
        /// </summary>
        /// <param name="searchTerm">The term we're searching for.</param>
        /// <param name="filter">Filter for whether to include prerelease, delisted, supportedframework flags in query.</param>
        /// <param name="skip">Skip how many items from beginning of list.</param>
        /// <param name="take">Return how many items.</param>
        /// <param name="log">Logger instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>List of package meta data.</returns>
        public override async Task<IEnumerable<IPackageSearchMetadata>> SearchAsync(string searchTerm, SearchFilter filter, int skip, int take, Common.ILogger log, CancellationToken cancellationToken)
        {
            var metadataCache = new MetadataReferenceCache();
            var searchResultMetadata = await _rawSearchResource.Search(
                    searchTerm,
                    filter,
                    skip,
                    take,
                    Common.NullLogger.Instance,
                    cancellationToken);

            var searchResults = searchResultMetadata
                .Select(m => m.WithVersions(() => GetVersions(m, filter)))
                .Select(m => metadataCache.GetObject((PackageSearchMetadataBuilder.ClonedPackageSearchMetadata) m))
                .ToArray();

            return searchResults;
        }

        private static IEnumerable<VersionInfo> GetVersions(PackageSearchMetadata metadata, SearchFilter filter)
        {
            var versions = metadata.ParsedVersions;

            // TODO: in v2, we only have download count for all versions, not per version.
            // To be consistent, in v3, we also use total download count for now.
            var totalDownloadCount = versions.Select(v => v.DownloadCount).Sum();
            versions = versions
                .Select(v => v.Version)
                .Where(v => filter.IncludePrerelease || !v.IsPrerelease)
                .Concat(new[] { metadata.Version })
                .Distinct()
                .Select(v => new VersionInfo(v, totalDownloadCount))
                .ToArray();

            return versions;
        }
    }
}
