﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.CommandLineUtils;
using Moq;
using NuGet.CommandLine.XPlat;
using NuGet.Commands;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.ProjectModel;
using NuGet.Test.Utility;
using NuGet.Versioning;
using Xunit;

namespace NuGet.XPlat.FuncTest
{
    public class XPlatAddPkgTests
    {
        private static readonly string projectName = "test_project_addpkg";

        private static MSBuildAPIUtility MsBuild
        {
            get { return new MSBuildAPIUtility(new TestCommandOutputLogger()); }
        }

        // Argument parsing related tests

        [Theory]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "--dg-file", "dgfile_foo", "--project", "project_foo", "", "", "", "", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "", "", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "--framework", "net46;netcoreapp1.0", "", "", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "-f", "net46 ; netcoreapp1.0 ; ", "", "", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "-f", "net46", "", "", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "--source", "a;b", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "-s", "a ; b ;", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "-s", "a", "", "", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "", "", "--package-directory", @"foo\dir", "")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "", "", "", "", "--no-restore")]
        [InlineData("--package", "package_foo", "--version", "1.0.0-foo", "-d", "dgfile_foo", "-p", "project_foo", "", "", "", "", "", "", "-n")]
        public void AddPkg_ArgParsing(string packageOption, string package, string versionOption, string version, string dgFileOption,
        string dgFilePath, string projectOption, string project, string frameworkOption, string frameworkString, string sourceOption,
        string sourceString, string packageDirectoryOption, string packageDirectory, string noRestoreSwitch)
        {
            // Arrange

            var argList = new List<string>() {
                "add",
                packageOption,
                package,
                versionOption,
                version,
                dgFileOption,
                dgFilePath,
                projectOption,
                project};

            if (!string.IsNullOrEmpty(frameworkOption))
            {
                argList.Add(frameworkOption);
                argList.Add(frameworkString);
            }
            if (!string.IsNullOrEmpty(sourceOption))
            {
                argList.Add(sourceOption);
                argList.Add(sourceString);
            }
            if (!string.IsNullOrEmpty(packageDirectoryOption))
            {
                argList.Add(packageDirectoryOption);
                argList.Add(packageDirectory);
            }
            if (!string.IsNullOrEmpty(noRestoreSwitch))
            {
                argList.Add(noRestoreSwitch);
            }

            var logger = new TestCommandOutputLogger();
            var testApp = new CommandLineApplication();
            var mockCommandRunner = new Mock<IPackageReferenceCommandRunner>();
            mockCommandRunner
                .Setup(m => m.ExecuteCommand(It.IsAny<PackageReferenceArgs>(), It.IsAny<MSBuildAPIUtility>()))
                .ReturnsAsync(0);

            testApp.Name = "dotnet nuget_test";
            AddPackageReferenceCommand.Register(testApp,
                () => logger,
                () => mockCommandRunner.Object);

            // Act
            var result = testApp.Execute(argList.ToArray());

            // Assert
            mockCommandRunner.Verify(m => m.ExecuteCommand(It.Is<PackageReferenceArgs>(p => p.PackageDependency.Id == package &&
            p.PackageDependency.VersionRange.OriginalString == version &&
            p.ProjectPath == project &&
            p.DgFilePath == dgFilePath &&
            p.NoRestore == !string.IsNullOrEmpty(noRestoreSwitch) &&
            (string.IsNullOrEmpty(frameworkOption) || !string.IsNullOrEmpty(frameworkOption) && p.Frameworks.SequenceEqual(MSBuildStringUtility.Split(frameworkString))) &&
            (string.IsNullOrEmpty(sourceOption) || !string.IsNullOrEmpty(sourceOption) && p.Sources.SequenceEqual(MSBuildStringUtility.Split(sourceString))) &&
            (string.IsNullOrEmpty(packageDirectoryOption) || !string.IsNullOrEmpty(packageDirectoryOption) && p.PackageDirectory == packageDirectory)),
            It.IsAny<MSBuildAPIUtility>()));

            Assert.Equal(0, result);
        }

        // Add Related Tests

        [Theory]
        [InlineData("1.0.0")]
        [InlineData("*")]
        [InlineData("1.*")]
        [InlineData("1.0.*")]
        public async void AddPkg_UnconditionalAdd_Success(string userInputVersion)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46");
                var packageX = XPlatTestUtils.CreatePackage();

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersion, projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild).Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForAllFrameworks(projectXmlRoot);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, userInputVersion));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "1.*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "1.*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "1.*")]
        [InlineData("net46", "net46; netcoreapp1.0", "1.*")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "1.*")]
        public async void AddPkg_UnconditionalAddWithNoRestore_Success(string packageFrameworks,
            string projectFrameworks,
            string userInputVersion)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, projectFrameworks);
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersion, projectA, noRestore: true);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // If noRestore is set, then we do not perform compatibility check.
                // The added package reference will be unconditional
                var itemGroup = XPlatTestUtils.GetItemGroupForAllFrameworks(projectXmlRoot);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, userInputVersion));
            }
        }

        [Fact]
        public async void AddPkg_UnconditionalAddWithoutVersion_Success()
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46");
                var packageX = XPlatTestUtils.CreatePackage();

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                // Since user is not inputing a version, it is converted to a "*"
                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, "*", projectA, noVersion: true);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                Assert.Equal(0, result);

                // Since user did not specify a version, the package reference will contain the resolved version
                Assert.True(XPlatTestUtils.ValidateReference(projectXmlRoot, packageX.Id, "1.0.0"));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "1.0.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "*")]
        public async void AddPkg_ConditionalAddWithoutUserInputFramework_Success(string packageFrameworks,
            string projectFrameworks, string userInputVersion)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, projectFrameworks);
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersion, projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();
                var commonFramework = XPlatTestUtils.GetCommonFramework(packageFrameworks, projectFrameworks);

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForFramework(projectXmlRoot, commonFramework);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, userInputVersion));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        public async void AddPkg_ConditionalAddWithUserInputFramework_Success(string packageFrameworks, string projectFrameworks, string userInputFrameworks)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46; netcoreapp1.0");
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, packageX.Version, projectA,
                    frameworks: userInputFrameworks);
                var commandRunner = new AddPackageReferenceCommandRunner();
                var commonFramework = XPlatTestUtils.GetCommonFramework(packageFrameworks, projectFrameworks, userInputFrameworks);

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForFramework(projectXmlRoot, commonFramework);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, packageX.Version));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        public async void AddPkg_ConditionalAddWithoutVersion_Success(string packageFrameworks,
            string projectFrameworks,
            string userInputFrameworks)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, projectFrameworks);
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                // Since user is not inputing a version, it is converted to a "*" in the command
                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, "*",
                    projectA,
                    frameworks: userInputFrameworks,
                    noVersion: true);

                var commandRunner = new AddPackageReferenceCommandRunner();
                var commonFramework = XPlatTestUtils.GetCommonFramework(packageFrameworks, projectFrameworks, userInputFrameworks);

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForFramework(projectXmlRoot, commonFramework);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);

                // Since user did not specify a version, the package reference will contain the resolved version
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, "1.0.0"));
            }
        }

        [Theory]
        [InlineData("net46", "netcoreapp1.0")]
        [InlineData("netcoreapp1.0", "net46")]
        [InlineData("net46", "unknown_framework")]
        [InlineData("netcoreapp1.0", "unknown_framework")]
        [InlineData("net46; netcoreapp1.0", "unknown_framework")]
        public async void AddPkg_FailureIncompatibleFrameworks(string packageFrameworks, string userInputFrameworks)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46; netcoreapp1.0");
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, packageX.Version, projectA,
                    frameworks: userInputFrameworks);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                Assert.Equal(1, result);
                Assert.True(XPlatTestUtils.ValidateNoReference(projectXmlRoot, packageX.Id));
            }
        }

        [Fact]
        public async void AddPkg_FailureUnknownPackage()
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46; netcoreapp1.0");
                var packageX = XPlatTestUtils.CreatePackage();

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs("unknown_package_id", "1.0.0", projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                Assert.Equal(1, result);
                Assert.True(XPlatTestUtils.ValidateNoReference(projectXmlRoot, packageX.Id));
                Assert.True(XPlatTestUtils.ValidateNoReference(projectXmlRoot, "unknown_package_id"));
            }
        }

        [Fact]
        public async void AddPkg_UnconditionalAddTwoPackages_Success()
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46");
                var packageX = XPlatTestUtils.CreatePackage("PkgX");
                var packageY = XPlatTestUtils.CreatePackage("PkgY");

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageY);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, packageX.Version, projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageY.Id, packageY.Version, projectA);

                // Act
                result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                Assert.Equal(0, result);
                Assert.True(XPlatTestUtils.ValidateTwoReferences(projectXmlRoot, packageX, packageY));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0")]
        public async void AddPkg_ConditionalAddTwoPackages_Success(string packageFrameworks, string projectFrameworks, string userInputFrameworks)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, projectFrameworks);
                var packageX = XPlatTestUtils.CreatePackage("PkgX", frameworkString: packageFrameworks);
                var packageY = XPlatTestUtils.CreatePackage("PkgY", frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageY);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id,
                    packageX.Version,
                    projectA,
                    frameworks: userInputFrameworks);
                var commandRunner = new AddPackageReferenceCommandRunner();
                var msBuild = MsBuild;
                var commonFramework = XPlatTestUtils.GetCommonFramework(packageFrameworks,
                    projectFrameworks,
                    userInputFrameworks);

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageY.Id, packageY.Version, projectA);

                // Act
                result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForFramework(projectXmlRoot, commonFramework);

                // Assert
                Assert.Equal(0, result);
                Assert.True(XPlatTestUtils.ValidateTwoReferences(projectXmlRoot, packageX, packageY));
            }
        }

        [Fact]
        public async void AddPkg_UnconditionalAddWithPackageDirectory_Success()
        {
            // Arrange

            using (var tempGlobalPackagesDirectory = TestDirectory.Create())
            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46");
                var packageX = XPlatTestUtils.CreatePackage();

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id,
                    packageX.Version,
                    projectA,
                    packageDirectory: tempGlobalPackagesDirectory.Path);
                var commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                var result = commandRunner.ExecuteCommand(packageArgs, MsBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;
                var itemGroup = XPlatTestUtils.GetItemGroupForAllFrameworks(projectXmlRoot);

                // Assert
                Assert.Equal(0, result);
                Assert.NotNull(itemGroup);
                Assert.True(XPlatTestUtils.ValidateReference(itemGroup, packageX.Id, packageX.Version));

                // Since user provided packge directory, assert if package is present
                Assert.True(XPlatTestUtils.ValidatePackageDownload(tempGlobalPackagesDirectory.Path, packageX));
            }
        }

        // Update Related Tests

        [Theory]
        [InlineData("0.0.5", "1.0.0")]
        [InlineData("0.0.5", "0.9")]
        [InlineData("0.0.5", "*")]
        [InlineData("*", "1.0.0")]
        [InlineData("*", "0.9")]
        [InlineData("*", "1.*")]
        public async void AddPkg_UnconditionalAddAsUpdate_Succcess(string userInputVersionOld, string userInputVersionNew)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, "net46; netcoreapp1.0");
                var packageX = XPlatTestUtils.CreatePackage();

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersionOld, projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();
                var msBuild = MsBuild;

                // Create a package ref with the old version
                var result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersionNew, projectA);
                commandRunner = new AddPackageReferenceCommandRunner();

                // Act
                // Create a package ref with the new version
                result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                // Verify that the only package reference is with the new version
                Assert.Equal(0, result);
                Assert.True(XPlatTestUtils.ValidateReference(projectXmlRoot, packageX.Id, userInputVersionNew));
            }
        }

        [Theory]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "0.0.5", "1.0.0")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "0.0.5", "1.0.0")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "0.0.5", "1.0.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "1.0.0")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "1.0.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "0.0.5", "0.9")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "0.0.5", "0.9")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "0.0.5", "0.9")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "0.9")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "0.9")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "0.0.5", "*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "0.0.5", "*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "0.0.5", "*")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "*")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "0.0.5", "*")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "*", "1.0.0")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "*", "1.0.0")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "*", "1.0.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "1.0.0")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "1.0.0")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "*", "0.9")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "*", "0.9")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "*", "0.9")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "0.9")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "0.9")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46", "*", "1.*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "net46", "*", "1.*")]
        [InlineData("net46; netcoreapp1.0", "net46; netcoreapp1.0", "netcoreapp1.0", "*", "1.*")]
        [InlineData("net46", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "1.*")]
        [InlineData("netcoreapp1.0", "net46; netcoreapp1.0", "net46; netcoreapp1.0", "*", "1.*")]
        public async void AddPkg_ConditionalAddAsUpdate_Succcess(string packageFrameworks, string projectFrameworks,
            string userInputFrameworks, string userInputVersionOld, string userInputVersionNew)
        {
            // Arrange

            using (var pathContext = new SimpleTestPathContext())
            {
                var projectA = XPlatTestUtils.CreateProject(projectName, pathContext, projectFrameworks);
                var packageX = XPlatTestUtils.CreatePackage(frameworkString: packageFrameworks);

                // Generate Package
                await SimpleTestPackageUtility.CreateFolderFeedV3(
                    pathContext.PackageSource,
                    PackageSaveMode.Defaultv3,
                    packageX);

                var packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersionOld, projectA);
                var commandRunner = new AddPackageReferenceCommandRunner();
                var msBuild = MsBuild;

                // Create a package ref with old version
                var result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                var projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                packageArgs = XPlatTestUtils.GetPackageReferenceArgs(packageX.Id, userInputVersionNew, projectA);
                commandRunner = new AddPackageReferenceCommandRunner();
                var commonFramework = XPlatTestUtils.GetCommonFramework(packageFrameworks, projectFrameworks, userInputFrameworks);

                // Act
                // Create a package ref with new version
                result = commandRunner.ExecuteCommand(packageArgs, msBuild)
                    .Result;
                projectXmlRoot = XPlatTestUtils.LoadCSProj(projectA.ProjectPath).Root;

                // Assert
                // Verify that the only package reference is with the new version
                Assert.Equal(0, result);
                Assert.True(XPlatTestUtils.ValidateReference(projectXmlRoot, packageX.Id, userInputVersionNew));
            }
        }
    }
}