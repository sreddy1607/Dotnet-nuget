﻿using System;
using System.Threading.Tasks;
// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NuGet.Common;

namespace NuGet.Build
{
    /// <summary>
    /// TaskLoggingHelper -> ILogger
    /// </summary>
    internal class MSBuildLogger : LoggerBase, Common.ILogger
    {
        private readonly TaskLoggingHelper _taskLogging;

        private delegate void LogMessageWithDetails(string subcategory, 
            string code, 
            string helpKeyword, 
            string file, 
            int lineNumber, 
            int columnNumber, 
            int endLineNumber, 
            int endColumnNumber, 
            MessageImportance importance, 
            string message, 
            params object[] messageArgs);

        private delegate void LogErrorWithDetails(string subcategory,
            string code,
            string helpKeyword,
            string file,
            int lineNumber,
            int columnNumber,
            int endLineNumber,
            int endColumnNumber,
            string message,
            params object[] messageArgs);

        private delegate void LogMessageAsString(MessageImportance importance, 
            string message, 
            params object[] messageArgs);

        private delegate void LogErrorAsString(string message,
            params object[] messageArgs);




        public MSBuildLogger(TaskLoggingHelper taskLogging)
        {
            _taskLogging = taskLogging;
        }

        public override void Log(ILogMessage message)
        {
            var logMessage = message as IRestoreLogMessage;

            if (logMessage == null)
            {
                logMessage = new RestoreLogMessage(message.Level, message.Message)
                {
                    Code = message.Code,
                    FilePath = message.ProjectPath,
                    StartLineNumber = -1,
                    EndLineNumber = -1,
                    StartColumnNumber = -1,
                    EndColumnNumber = -1
                };
            }

            switch (message.Level)
            {
                case LogLevel.Error:
                    LogError(logMessage, _taskLogging.LogError, _taskLogging.LogError);
                    break;

                case LogLevel.Warning:
                    LogError(logMessage, _taskLogging.LogError, _taskLogging.LogError);
                    break;

                case LogLevel.Minimal:
                    LogMessage(logMessage, MessageImportance.High, _taskLogging.LogMessage, _taskLogging.LogMessage);
                    break;

                case LogLevel.Information:
                    LogMessage(logMessage, MessageImportance.Normal, _taskLogging.LogMessage, _taskLogging.LogMessage);
                    break;

                case LogLevel.Debug:
                case LogLevel.Verbose:
                default:
                    // Default to LogLevel.Debug and low importance
                    LogMessage(logMessage, MessageImportance.Low, _taskLogging.LogMessage, _taskLogging.LogMessage);
                    break;

            }
        }

        private void LogMessage(IRestoreLogMessage logMessage, 
            MessageImportance importance,
            LogMessageWithDetails logWithDetails, 
            LogMessageAsString logAsString)
        {
            if (logMessage.Code > NuGetLogCode.Undefined)
            {
                logWithDetails(string.Empty,
                    Enum.GetName(typeof(NuGetLogCode), logMessage.Code),
                    Enum.GetName(typeof(NuGetLogCode), logMessage.Code),
                    logMessage.FilePath,
                    logMessage.StartLineNumber,
                    logMessage.StartColumnNumber,
                    logMessage.EndLineNumber,
                    logMessage.EndColumnNumber,
                    importance,
                    logMessage.Message);
            }
            else
            {
                logAsString(importance, logMessage.Message);
            }
        }

        private void LogError(IRestoreLogMessage logMessage,
            LogErrorWithDetails logWithDetails,
            LogErrorAsString logAsString)
        {
            if (logMessage.Code > NuGetLogCode.Undefined)
            {
                logWithDetails(string.Empty,
                    Enum.GetName(typeof(NuGetLogCode), logMessage.Code),
                    Enum.GetName(typeof(NuGetLogCode), logMessage.Code),
                    logMessage.FilePath,
                    logMessage.StartLineNumber,
                    logMessage.StartColumnNumber,
                    logMessage.EndLineNumber,
                    logMessage.EndColumnNumber,
                    logMessage.Message);
            }
            else
            {
                logAsString(logMessage.Message);
            }
        }

        public override System.Threading.Tasks.Task LogAsync(ILogMessage message)
        {
            Log(message);

            return System.Threading.Tasks.Task.FromResult(0);
        }
    }
}