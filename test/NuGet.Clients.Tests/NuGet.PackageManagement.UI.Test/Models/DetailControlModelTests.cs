// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ServiceHub.Framework;
using Microsoft.VisualStudio.Threading;
using Moq;
using NuGet.Packaging;
using NuGet.Test.Utility;
using NuGet.Versioning;
using NuGet.VisualStudio;
using NuGet.VisualStudio.Internal.Contracts;
using Xunit;

namespace NuGet.PackageManagement.UI.Test.Models
{
    public abstract class DetailControlModelTestBase : IClassFixture<LocalPackageSearchMetadataFixture>, IDisposable
    {
        protected readonly LocalPackageSearchMetadataFixture _testData;
        protected readonly PackageItemListViewModel _testViewModel;
        protected readonly JoinableTaskContext _joinableTaskContext;
        protected bool disposedValue = false;

        public DetailControlModelTestBase(LocalPackageSearchMetadataFixture testData)
        {
            _testData = testData;
            var testVersion = new NuGetVersion(0, 0, 1);
            _testViewModel = new PackageItemListViewModel()
            {
                PackageReader = _testData.TestData.PackageReader,
                Version = testVersion,
                InstalledVersion = testVersion,
            };

#pragma warning disable VSSDK005 // Avoid instantiating JoinableTaskContext
            _joinableTaskContext = new JoinableTaskContext(Thread.CurrentThread, SynchronizationContext.Current);
#pragma warning restore VSSDK005 // Avoid instantiating JoinableTaskContext
            NuGetUIThreadHelper.SetCustomJoinableTaskFactory(_joinableTaskContext.Factory);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _joinableTaskContext?.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
    }

    public class PackageDetailControlModelTests : DetailControlModelTestBase
    {
        private readonly PackageDetailControlModel _testInstance;

        public PackageDetailControlModelTests(LocalPackageSearchMetadataFixture testData)
            : base(testData)
        {
            var solMgr = new Mock<INuGetSolutionManagerService>();
            _testInstance = new PackageDetailControlModel(
                solutionManager: solMgr.Object,
                projects: new List<IProjectContextInfo>());

            _testInstance.SetCurrentPackage(
                _testViewModel,
                ItemFilter.All,
                () => null).Wait();
        }

        [Fact]
        public void PackageReader_Always_IsNotNull()
        {
            Assert.NotNull(_testInstance.PackageReader);

            Func<PackageReaderBase> lazyReader = _testInstance.PackageReader;

            PackageReaderBase reader = lazyReader();
            Assert.IsType(typeof(PackageArchiveReader), reader);
        }

        [Theory]
        [InlineData(NuGetProjectKind.Unknown)]
        [InlineData(NuGetProjectKind.PackageReference)]
        [InlineData(NuGetProjectKind.ProjectK)]
        public void Options_ShowClassicOptions_WhenProjectKindIsNotProjectConfig_ReturnsFalse(NuGetProjectKind projectKind)
        {
            var project = new Mock<IProjectContextInfo>();

            project.SetupGet(p => p.ProjectKind)
                .Returns(projectKind);

            var model = new PackageDetailControlModel(
                solutionManager: Mock.Of<INuGetSolutionManagerService>(),
                projects: new[] { project.Object });

            Assert.False(model.Options.ShowClassicOptions);
        }

        [Fact]
        public void Options_ShowClassicOptions_WhenProjectKindIsProjectConfig_ReturnsTrue()
        {
            var project = new Mock<IProjectContextInfo>();

            project.SetupGet(p => p.ProjectKind)
                .Returns(NuGetProjectKind.PackagesConfig);

            var model = new PackageDetailControlModel(
                solutionManager: Mock.Of<INuGetSolutionManagerService>(),
                projects: new[] { project.Object });

            Assert.True(model.Options.ShowClassicOptions);
        }
    }

    public class PackageSolutionDetailControlModelTests : DetailControlModelTestBase
    {
        private PackageSolutionDetailControlModel _testInstance;

        public PackageSolutionDetailControlModelTests(LocalPackageSearchMetadataFixture testData)
            : base(testData)
        {
            var solMgr = new Mock<INuGetSolutionManagerService>();
            var serviceBroker = new Mock<IServiceBroker>();
            var projectManagerService = new Mock<INuGetProjectManagerService>();
            projectManagerService.Setup(x => x.GetProjectsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<IProjectContextInfo>());

#pragma warning disable ISB001 // Dispose of proxies
            serviceBroker.Setup(x => x.GetProxyAsync<INuGetProjectManagerService>(It.Is<ServiceJsonRpcDescriptor>(d => d.Moniker == NuGetServices.ProjectManagerService.Moniker), It.IsAny<ServiceActivationOptions>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(projectManagerService.Object);
#pragma warning restore ISB001 // Dispose of proxies

            NuGetUIThreadHelper.JoinableTaskFactory.Run(async () =>
            {
                _testInstance = await PackageSolutionDetailControlModel.CreateAsync(
                    solutionManager: solMgr.Object,
                    projects: new List<IProjectContextInfo>(),
                    packageManagerProviders: new List<IVsPackageManagerProvider>(),
                    serviceBroker: serviceBroker.Object,
                    CancellationToken.None);
            });

            _testInstance.SetCurrentPackage(
                _testViewModel,
                ItemFilter.All,
                () => null).Wait();
        }

        [Fact]
        public void PackageReader_Always_IsNotNull()
        {
            Assert.NotNull(_testInstance.PackageReader);

            Func<PackageReaderBase> lazyReader = _testInstance.PackageReader;

            PackageReaderBase reader = lazyReader();
            Assert.IsType(typeof(PackageArchiveReader), reader);
        }
    }
}
