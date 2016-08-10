﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using NuGet.Protocol.Core.Types;
using NuGet.Test.Utility;
using NuGet.Versioning;
using Xunit;

namespace NuGet.PackageManagement.UI.Test
{
    public class UpdatePackageFeedTests
    {
        private readonly MultiSourcePackageMetadataProvider _metadataProvider;
        private readonly PackageMetadataResource _metadataResource;

        public UpdatePackageFeedTests()
        {
            // dependencies and data
            _metadataResource = Mock.Of<PackageMetadataResource>();

            var provider = Mock.Of<INuGetResourceProvider>();
            Mock.Get(provider)
                .Setup(x => x.TryCreate(It.IsAny<SourceRepository>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(Tuple.Create(true, (INuGetResource)_metadataResource)));
            Mock.Get(provider)
                .Setup(x => x.ResourceType)
                .Returns(typeof(PackageMetadataResource));

            var logger = new TestLogger();
            var packageSource = new Configuration.PackageSource("http://fake-source");
            var source = new SourceRepository(packageSource, new[] { provider });

            // target
            _metadataProvider = new MultiSourcePackageMetadataProvider(
                new[] { source },
                optionalLocalRepository: null,
                optionalGlobalLocalRepositories: null,
                logger: logger);
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithAllVersions_RetrievesSingleUpdate()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", new NuGetVersion("1.0.0"));

            var projectA = SetupProject("FakePackage", "1.0.0");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            Assert.Single(packages);
            var updateCandidate = packages.Single();
            Assert.Equal("2.0.1", updateCandidate.Identity.Version.ToString());

            var actualVersions = await updateCandidate.GetVersionsAsync();
            Assert.NotEmpty(actualVersions);
            Assert.Equal(
                new[] { "2.0.1", "2.0.0", "1.0.1", "1.0.0", "0.0.1" },
                actualVersions.Select(v => v.Version.ToString()).ToArray());
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithAllowedVersions_RetrievesSingleUpdate()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", new NuGetVersion("1.0.0"));

            var projectA = SetupProject("FakePackage", "1.0.0", "[1,2)");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            Assert.Single(packages);
            var updateCandidate = packages.Single();
            Assert.Equal("1.0.1", updateCandidate.Identity.Version.ToString());

            var actualVersions = await updateCandidate.GetVersionsAsync();
            Assert.NotEmpty(actualVersions);
            Assert.Equal(
                new[] { "2.0.1", "2.0.0", "1.0.1", "1.0.0", "0.0.1" },
                actualVersions.Select(v => v.Version.ToString()).ToArray());
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithMultipleProjects_RetrievesSingleUpdate1()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", new NuGetVersion("1.0.0"));

            // Both projects need to be updated to different versions
            // projectA: 1.0.0 => 2.0.1
            // projectB: 1.0.0 => 1.0.1
            var projectA = SetupProject("FakePackage", "1.0.0");
            var projectB = SetupProject("FakePackage", "1.0.0", "[1,2)");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA, projectB }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            // Should retrieve a single update item with the lowest version and full list of available versions
            Assert.Single(packages);
            var updateCandidate = packages.Single();
            Assert.Equal("1.0.1", updateCandidate.Identity.Version.ToString());

            var actualVersions = await updateCandidate.GetVersionsAsync();
            Assert.NotEmpty(actualVersions);
            Assert.Equal(
                new[] { "2.0.1", "2.0.0", "1.0.1", "1.0.0", "0.0.1" },
                actualVersions.Select(v => v.Version.ToString()).ToArray());
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithMultipleProjects_RetrievesSingleUpdate2()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", NuGetVersion.Parse("1.0.0"));

            // Only one project needs to be updated
            // projectA: 2.0.0 => 2.0.1
            // projectB: 1.0.1 => None
            var projectA = SetupProject("FakePackage", "2.0.0");
            var projectB = SetupProject("FakePackage", "1.0.1", "[1,2)");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA, projectB }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            // Should retrieve a single update item with the lowest version and full list of available versions
            Assert.Single(packages);
            var updateCandidate = packages.Single();
            Assert.Equal("2.0.1", updateCandidate.Identity.Version.ToString());

            var actualVersions = await updateCandidate.GetVersionsAsync();
            Assert.NotEmpty(actualVersions);
            Assert.Equal(
                new[] { "2.0.1", "2.0.0", "1.0.1", "1.0.0", "0.0.1" },
                actualVersions.Select(v => v.Version.ToString()).ToArray());
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithMultipleProjects_RetrievesSingleUpdate3()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", NuGetVersion.Parse("1.0.0"));

            // Only one project needs to be updated
            // projectA: 2.0.1 => None
            // projectB: 1.0.0 => 1.0.1
            var projectA = SetupProject("FakePackage", "2.0.1");
            var projectB = SetupProject("FakePackage", "1.0.0", "[1,2)");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA, projectB }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            // Should retrieve a single update item with the lowest version and full list of available versions
            Assert.Single(packages);
            var updateCandidate = packages.Single();
            Assert.Equal("1.0.1", updateCandidate.Identity.Version.ToString());

            var actualVersions = await updateCandidate.GetVersionsAsync();
            Assert.NotEmpty(actualVersions);
            Assert.Equal(
                new[] { "2.0.1", "2.0.0", "1.0.1", "1.0.0", "0.0.1" },
                actualVersions.Select(v => v.Version.ToString()).ToArray());
        }

        [Fact]
        public async Task GetPackagesWithUpdatesAsync_WithMultipleProjects_RetrievesNoUpdates()
        {
            // Arrange
            var testPackageIdentity = new PackageIdentity("FakePackage", NuGetVersion.Parse("1.0.0"));

            var projectA = SetupProject("FakePackage", "2.0.1");
            var projectB = SetupProject("FakePackage", "1.0.1", "[1,2)");
            SetupRemotePackageMetadata("FakePackage", "0.0.1", "1.0.0", "2.0.1", "2.0.0", "1.0.1");

            var _target = new UpdatePackageFeed(new[] { testPackageIdentity }, _metadataProvider, new[] { projectA, projectB }, null, new TestLogger());

            // Act
            var packages = await _target.GetPackagesWithUpdatesAsync(
                "fake", new SearchFilter(), CancellationToken.None);

            // Assert
            Assert.Empty(packages);
        }

        private NuGetProject SetupProject(string packageId, string packageVersion, string allowedVersions = null)
        {
            var packageIdentity = new PackageIdentity(packageId, NuGetVersion.Parse(packageVersion));

            var installedPackages = new[]
            {
                new PackageReference(
                    packageIdentity,
                    NuGetFramework.Parse("net45"),
                    userInstalled: true,
                    developmentDependency: false,
                    requireReinstallation: false,
                    allowedVersions: allowedVersions != null ? VersionRange.Parse(allowedVersions) : null)
            };

            var project = Mock.Of<NuGetProject>();
            Mock.Get(project)
                .Setup(x => x.GetInstalledPackagesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<IEnumerable<PackageReference>>(installedPackages));
            return project;
        }

        private void SetupRemotePackageMetadata(string id, params string[] versions)
        {
            var metadata = versions
                .Select(v => PackageSearchMetadataBuilder
                    .FromIdentity(new PackageIdentity(id, new NuGetVersion(v)))
                    .Build());

            Mock.Get(_metadataResource)
                .Setup(x => x.GetMetadataAsync(id, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<Common.ILogger>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(metadata));
        }
    }
}
