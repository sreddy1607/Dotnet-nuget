﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Moq;
using Xunit;

namespace NuGet.Common.Test
{
    public class CollectorLoggerTests
    {

        [Fact]
        public void CollectorLogger_DoesNotPassLogMessagesToInnerLoggerByDefault()
        {

            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: true);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug"));
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose"));
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information"));
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning"));
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error"));

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Never());
        }

        [Fact]
        public void CollectorLogger_DoesNotPassLogCallsToInnerLoggerByDefault()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: true);

            // Act
            collector.Log(LogLevel.Debug, "Debug");
            collector.Log(LogLevel.Verbose, "Verbose");
            collector.Log(LogLevel.Information, "Information");
            collector.Log(LogLevel.Warning, "Warning");
            collector.Log(LogLevel.Error, "Error");

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Never());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerOnlyWithShouldDisplay()
        {

            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: true);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = false });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = false });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = false });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = true });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerWithShouldDisplay()
        {

            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: true);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = true });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerWithNoShouldDisplayAndHideWarningsAndErrors()
        {

            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: false);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = false });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = false });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerWithShouldDisplayAndHideWarningsAndErrors()
        {

            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Debug, hideWarningsAndErrors: false);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = true });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerWithLessVerbosity()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Verbose, hideWarningsAndErrors: true);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = true });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Once());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }

        [Fact]
        public void CollectorLogger_PassesLogMessagesToInnerLoggerWithLeastVerbosity()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object, LogLevel.Error, hideWarningsAndErrors: false);

            // Act
            collector.Log(new RestoreLogMessage(LogLevel.Debug, "Debug") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Verbose, "Verbose") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Information, "Information") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Warning, "Warning") { ShouldDisplay = true });
            collector.Log(new RestoreLogMessage(LogLevel.Error, "Error") { ShouldDisplay = true });

            // Assert
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Debug, "Debug", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Verbose, "Verbose", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Information, "Information", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Warning, "Warning", Times.Never());
            VerifyInnerLoggerCalls(innerLogger, LogLevel.Error, "Error", Times.Once());
        }


        [Fact]
        public void CollectorLogger_CollectsErrors()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object);

            // Act
            var errorsStart = collector.Errors.ToArray();
            collector.LogError("ErrorA");
            var errorsA = collector.Errors.ToArray();
            collector.LogError("ErrorB");
            collector.LogError("ErrorC");
            var errorsAbc = collector.Errors.ToArray();
            var errordEnd = collector.Errors.ToArray();

            // Assert
            Assert.Empty(errorsStart);
            Assert.Equal(new[] { "ErrorA" }, errorsA.Select(e => e.Message));
            Assert.Equal(new[] { "ErrorA", "ErrorB", "ErrorC" }, errorsAbc.Select(e => e.Message));
            Assert.Equal(new[] { "ErrorA", "ErrorB", "ErrorC" }, errordEnd.Select(e => e.Message));
        }

        [Fact]
        public void CollectorLogger_CollectsWarnings()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object);

            // Act
            var warningsStart = collector.Errors.ToArray();
            collector.LogWarning("WarningA");
            var warningA = collector.Errors.ToArray();
            collector.LogWarning("WarningB");
            collector.LogWarning("WarningC");
            var warningAbc = collector.Errors.ToArray();
            var warningsEnd = collector.Errors.ToArray();

            // Assert
            Assert.Empty(warningsStart);
            Assert.Equal(new[] { "WarningA" }, warningA.Select(e => e.Message));
            Assert.Equal(new[] { "WarningA", "WarningB", "WarningC" }, warningAbc.Select(e => e.Message));
            Assert.Equal(new[] { "WarningA", "WarningB", "WarningC" }, warningsEnd.Select(e => e.Message));
        }

        [Fact]
        public void CollectorLogger_CollectsOnlyErrorsAndWarnings()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object);

            // Act
            var warningsStart = collector.Errors.ToArray();
            collector.LogWarning("WarningA");
            collector.LogDebug("Debug");
            var warningA = collector.Errors.ToArray();
            collector.LogInformation("Information");
            collector.LogWarning("WarningB");
            collector.LogWarning("WarningC");
            var warningAbc = collector.Errors.ToArray();
            var warningsEnd = collector.Errors.ToArray();

            // Assert
            Assert.Empty(warningsStart);
            Assert.Equal(new[] { "WarningA" }, warningA.Select(e => e.Message));
            Assert.Equal(new[] { "WarningA", "WarningB", "WarningC" }, warningAbc.Select(e => e.Message));
            Assert.Equal(new[] { "WarningA", "WarningB", "WarningC" }, warningsEnd.Select(e => e.Message));
        }

        [Fact]
        public void CollectorLogger_DoesNotCollectNonErrorAndNonWarnings()
        {
            // Arrange
            var innerLogger = new Mock<ILogger>();
            var collector = new CollectorLogger(innerLogger.Object);

            // Act
            collector.LogDebug("Debug");
            collector.LogVerbose("Verbose");
            collector.LogInformation("Information");
            var errors = collector.Errors.ToArray();

            // Assert
            Assert.Empty(errors);
        }

        private void VerifyInnerLoggerCalls(Mock<ILogger> innerLogger, LogLevel messageLevel, string message, Times times)
        {
            innerLogger.Verify(x => x.Log(It.Is<ILogMessage>(l => 
            l.Level == messageLevel && 
            l.Message == message)), 
            times);
        }
    }
}
