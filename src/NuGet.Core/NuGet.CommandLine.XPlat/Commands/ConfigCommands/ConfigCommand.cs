// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.CommandLineUtils;
using NuGet.Common;

namespace NuGet.CommandLine.XPlat
{
    internal partial class ConfigCommand
    {
        internal static void Register(CommandLineApplication app,
                                      Func<ILogger> getLogger)
        {
            app.Command("config", ConfigCmd =>
            {
                ConfigCmd.Command("paths", PathsCmd =>
                {
                    CommandArgument workingdirectory = PathsCmd.Argument(
                        "working-directory",
                        Strings.ConfigPathsWorkingDirectoryDescription);
                    PathsCmd.HelpOption("-h|--help");
                    PathsCmd.Description = Strings.ConfigPathsCommandDescription;
                    PathsCmd.OnExecute(() =>
                    {
                        var args = new ConfigPathsArgs()
                        {
                            WorkingDirectory = workingdirectory.Value,
                        };

                        ConfigPathsRunner.Run(args, getLogger);
                        return 0;
                    });
                });
                ConfigCmd.HelpOption("-h|--help");
                ConfigCmd.Description = Strings.Config_Description;
                ConfigCmd.OnExecute(() =>
                {
                    app.ShowHelp("config");
                    return 0;
                });
            });
        }
    }
}
