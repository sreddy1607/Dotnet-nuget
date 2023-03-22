// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.IO;

using NuGet.Commands;
using NuGet.Common;
using NuGet.Configuration;

namespace NuGet.CommandLine.XPlat
{
    internal static class ConfigPathsRunner
    {
        public static void Run(ConfigPathsArgs args, Func<ILogger> getLogger)
        {
            var settings = RunnerHelper.GetSettingsFromDirectory(args.WorkingDirectory);
            ILogger logger = getLogger();

            var filePaths = settings.GetConfigFilePaths();
            foreach (var filePath in filePaths)
            {
                logger.LogMinimal(filePath);
            }
        }
    }

    internal static class ConfigGetRunner
    {
        public static void Run(ConfigGetArgs args, Func<ILogger> getLogger)
        {

            if (args.AllOrConfigKey.Equals("all", StringComparison.OrdinalIgnoreCase))
            {
                // Get All; will return something
            }

            var settings = RunnerHelper.GetSettingsFromDirectory(args.WorkingDirectory);
            ILogger logger = getLogger();

            var configValue = SettingsUtility.GetConfigValue(settings, args.AllOrConfigKey);
            if (string.IsNullOrEmpty(configValue))
            {
                // Need to add actual message to strings.resx
                logger.LogMinimal("Config key does not exist");
            }

            logger.LogMinimal(configValue);

        }
    }

    internal static class RunnerHelper
    {
        public static ISettings GetSettingsFromDirectory(string directory)
        {
            if (string.IsNullOrEmpty(directory))
            {
                directory = Directory.GetCurrentDirectory();
            }
            if (!Directory.Exists(directory))
            {
                throw new CommandException(string.Format(CultureInfo.CurrentCulture, Strings.Error_PathNotFound, directory));
            }

            return NuGet.Configuration.Settings.LoadDefaultSettings(
                directory,
                configFileName: null,
                machineWideSettings: new XPlatMachineWideSetting());
        }
    }
}
