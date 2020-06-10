// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.PackageManagement.VisualStudio;
using NuGet.ProjectManagement;
using Xunit.Abstractions;

namespace NuGet.PackageManagement.UI.Test
{
    /// <summary>
    /// NuGet UI Logger to print messages to Xunit test output
    /// </summary>
    class TestNuGetUILogger : INuGetUILogger
    {
        private readonly ITestOutputHelper _out;

        public TestNuGetUILogger(ITestOutputHelper outputHelper)
        {
            _out = outputHelper;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        public void Log(MessageLevel level, string message, params object[] args)
        {
            _ = LogAsync(level, message, args);
        }

        public Task LogAsync(MessageLevel level, string message, params object[] args)
        {
            _out.WriteLine($"[{level}] {string.Format(message, args)}");
            return new Task(() => { });
        }

        public void Log(ILogMessage message)
        {
            Log(FromLogLevel(message.Level), message.Message);
        }

        public Task LogAsync(ILogMessage message)
        {
            Log(FromLogLevel(message.Level), message.Message);
            return new Task(() => { });
        }

        public void ReportError(string message)
        {
            Log(MessageLevel.Error, message);
        }

        public void ReportError(ILogMessage message)
        {
            Log(MessageLevel.Error, message.Message);
        }

        public void End()
        {
            throw new NotImplementedException();
        }

        public Task EndAsync()
        {
            throw new NotImplementedException();
        }

        private MessageLevel FromLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Error:
                    return MessageLevel.Error;
                case LogLevel.Warning:
                    return MessageLevel.Warning;
                case LogLevel.Verbose:
                case LogLevel.Debug:
                    return MessageLevel.Debug;
                case LogLevel.Minimal:
                case LogLevel.Information:
                    return MessageLevel.Info;
                default:
                    return MessageLevel.Warning;
            }
        }
    }
}
