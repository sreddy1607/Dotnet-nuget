﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Common
{
    public class RestoreLogMessage : IRestoreLogMessage
    {
        public LogLevel Level { get; set; }
        public NuGetLogCode Code { get; set; }
        public string Message { get; set; }
        public IReadOnlyList<string> TargetGraphs { get; set; }
        public DateTimeOffset Time { get; set; }
        public string ProjectPath { get; set; }
        public WarningLevel WarningLevel { get; set; }
        public string FilePath { get; set; }
        public int LineNumber { get; set; } = -1;
        public int ColumnNumber { get; set; } = -1;
        public string LibraryId { get; set; }

        public RestoreLogMessage(LogLevel logLevel, NuGetLogCode errorCode, 
            string errorString, string targetGraph)
        {
            Level = logLevel;
            Code = errorCode;
            Message = errorString;
            Time = DateTimeOffset.Now;

            if (!string.IsNullOrEmpty(targetGraph))
            {
                TargetGraphs = new List<string>
                {
                    targetGraph
                };
            }
        }

        public RestoreLogMessage(LogLevel logLevel, NuGetLogCode errorCode, string errorString)
            : this(logLevel, errorCode, errorString, string.Empty)
        {

        }

        public RestoreLogMessage(LogLevel logLevel, string errorString)
            : this(logLevel, LogLevel.Error == logLevel ? NuGetLogCode.NU1000 : NuGetLogCode.NU1500, errorString, string.Empty)
        { 
        }

        public string FormatMessage()
        {
            // Only errors and warnings need codes. informational do not need codes.
            if(Level >= LogLevel.Warning)
            {
                return $"{Enum.GetName(typeof(NuGetLogCode), Code)}: {Message}";
            }
            else
            {
                return Message;
            }
        }

        public Task<string> FormatMessageAsync()
        {
            return Task.FromResult(FormatMessage());
        }

        /// <summary>
        /// Create a log message for a target graph library.
        /// </summary>
        public static RestoreLogMessage CreateWarning(
            NuGetLogCode code,
            string libraryId,
            string message,
            params string[] targetGraphs)
        {
            return new RestoreLogMessage(LogLevel.Warning, message)
            {
                Code = code,
                LibraryId = libraryId,
                TargetGraphs = targetGraphs.ToList()
            };
        }
    }
}
