// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NuGet.CommandLine.XPlat;
using NuGet.Configuration;
using NuGet.Test.Utility;
using Org.BouncyCastle.Asn1.Mozilla;
using Xunit;
using static NuGet.XPlat.FuncTest.XPlatClientCertTests.TestInfo;

namespace NuGet.XPlat.FuncTest
{
    [Collection("NuGet XPlat Config Test Collection")]
    public class XPlatConfigTests
    {

        [Fact]
        public void ConfigPathsCommand_ListConfigPathsWithArgs_Success()
        {
            // Arrange
            using (var testInfo = new TestInfo("NuGet.Config", ".test/work"))
            {
                var args = new[]
                {
                    "config",
                    "paths",
                    testInfo.WorkingPath
                };
                var log = new TestCommandOutputLogger();

                // Act
                var exitCode = Program.MainInternal(args.ToArray(), log);

                // Assert
                Assert.Equal(string.Empty, log.ShowErrors());
                Assert.Equal(0, exitCode);
                Assert.Contains(Path.Combine(testInfo.WorkingPath.Path, "NuGet.Config"), log.Messages);
            }

        }

            // Test for non-existing working directory argument

            // Test for inaccessible working directory argument

            // Test for displaying help message

            // Tests for displaying other error messages

        internal class TestInfo : IDisposable
        {
            public static void CreateFile(string directory, string fileName, string fileContent)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var fileFullName = Path.Combine(directory, fileName);
                CreateFile(fileFullName, fileContent);
            }

            public static void CreateFile(string fileFullName, string fileContent)
            {
                using (var writer = new StreamWriter(fileFullName))
                {
                    writer.Write(fileContent);
                }
            }

            public TestInfo(string configPath, string configDirectory)
            {
                WorkingPath = TestDirectory.Create();
                ConfigFile = configPath;
                //InvalidWorkingPath = @"C:\Test\NonExistingRepos";
                CreateFile(WorkingPath.Path,
                           Path.GetFileName(ConfigFile),
                           $@"
<configuration>
    <packageSources>
        <add key=""Foo"" value=""https://contoso.com/v3/index.json"" />
    </packageSources>
</configuration>
");
            }

            public TestDirectory WorkingPath { get; }
            public string ConfigFile { get; }
            //public string InvalidWorkingPath { get; }
            public void Dispose()
            {
                WorkingPath.Dispose();
            }
        }
    }
}
