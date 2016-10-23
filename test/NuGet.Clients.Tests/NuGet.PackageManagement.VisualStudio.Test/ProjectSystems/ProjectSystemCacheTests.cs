﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Moq;
using Xunit;

namespace NuGet.PackageManagement.VisualStudio.Test
{
    public class ProjectSystemCacheTests
    {
        [Fact]
        public void TryGetDTEProject_ReturnsProjectByFullName()
        {
            // Arrange
            var target = new ProjectSystemCache();
            var projectNames = new ProjectNames(
                fullName: @"C:\src\project\project.csproj",
                uniqueName: @"folder\project",
                shortName: "project",
                customUniqueName: @"folder\project");
            var dteProject = new Mock<EnvDTE.Project>();

            target.AddProject(projectNames, dteProject.Object, nuGetProject: null);
            EnvDTE.Project actual;

            // Act
            var success = target.TryGetDTEProject(projectNames.FullName, out actual);

            // Assert
            Assert.True(success, "The project should have been fetched from the cache by full name.");
            Assert.Same(dteProject.Object, actual);
        }

        [Fact]
        public void TryGetDTEProject_ReturnsProjectByCustomUniqueName()
        {
            // Arrange
            var target = new ProjectSystemCache();
            var projectNames = new ProjectNames(
                fullName: @"C:\src\project\project.csproj",
                uniqueName: @"folder\project",
                shortName: "project",
                customUniqueName: @"custom");
            var dteProject = new Mock<EnvDTE.Project>();

            target.AddProject(projectNames, dteProject.Object, nuGetProject: null);
            EnvDTE.Project actual;

            // Act
            var success = target.TryGetDTEProject(projectNames.CustomUniqueName, out actual);

            // Assert
            Assert.True(success, "The project should have been fetched from the cache by custom unique name.");
            Assert.Same(dteProject.Object, actual);
        }

        [Fact]
        public void TryGetDTEProject_ReturnsProjectByUniqueName()
        {
            // Arrange
            var target = new ProjectSystemCache();
            var projectNames = new ProjectNames(
                fullName: @"C:\src\project\project.csproj",
                uniqueName: @"folder\project",
                shortName: "project",
                customUniqueName: @"folder\project");
            var dteProject = new Mock<EnvDTE.Project>();

            target.AddProject(projectNames, dteProject.Object, nuGetProject: null);
            EnvDTE.Project actual;

            // Act
            var success = target.TryGetDTEProject(projectNames.UniqueName, out actual);

            // Assert
            Assert.True(success, "The project should have been fetched from the cache by unique name.");
            Assert.Same(dteProject.Object, actual);
        }

        [Fact]
        public void TryGetDTEProject_ReturnsProjectWhenShortNameIsNotAmbiguous()
        {
            // Arrange
            var target = new ProjectSystemCache();
            var projectNames = new ProjectNames(
                fullName: @"C:\src\project\project.csproj",
                uniqueName: @"folder\project",
                shortName: "project",
                customUniqueName: @"folder\project");
            var dteProject = new Mock<EnvDTE.Project>();

            target.AddProject(projectNames, dteProject.Object, nuGetProject: null);
            EnvDTE.Project actual;

            // Act
            var success = target.TryGetDTEProject(projectNames.ShortName, out actual);

            // Assert
            Assert.True(success, "The project should have been fetched from the cache by short name.");
            Assert.Same(dteProject.Object, actual);
        }

        [Fact]
        public void TryGetDTEProject_ReturnsNullWhenShortNameIsAmbiguous()
        {
            // Arrange
            var target = new ProjectSystemCache();

            var projectNamesA = new ProjectNames(
                fullName: @"C:\src\projectA\project.csproj",
                uniqueName: @"folderA\project",
                shortName: "project",
                customUniqueName: @"folderA\project");

            var projectNamesB = new ProjectNames(
                fullName: @"C:\src\projectB\project.csproj",
                uniqueName: @"folderB\project",
                shortName: projectNamesA.ShortName,
                customUniqueName: @"folderB\project");
            
            target.AddProject(projectNamesA, dteProject: null, nuGetProject: null);
            target.AddProject(projectNamesB, dteProject: null, nuGetProject: null);

            EnvDTE.Project actual;

            // Act
            var success = target.TryGetDTEProject(projectNamesA.ShortName, out actual);

            // Assert
            Assert.False(success, "The project should not have been fetched from the cache by short name.");
            Assert.Null(actual);
        }
    }
}
