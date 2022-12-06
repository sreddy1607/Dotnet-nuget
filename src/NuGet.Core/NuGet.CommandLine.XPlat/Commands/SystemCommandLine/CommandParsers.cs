// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// Do not manually edit this autogenerated file:
// instead modify the neighboring .tt file (text template) and/or NuGet.CommandLine.Xplat\Commands\SystemCommandLine\Commands.xml (data file),
// then re-execute the text template via "run custom tool" on VS context menu for .tt file, or via dotnet-t4 global tool.

using System;
using System.CodeDom.Compiler;
using System.CommandLine;
using NuGet.Common;

namespace NuGet.CommandLine.XPlat.Commands
{
    [GeneratedCode("CommandParsers.tt", "0.0.1")]
    internal static class CommandParsers
    {
        public static void Register(Command app, Func<ILogger> getLogger, Func<Exception, int> commandExceptionHandler)
        {
            AddVerbParser.Register(app, getLogger, commandExceptionHandler);
            ListVerbParser.Register(app, getLogger, commandExceptionHandler);
        }
    }
}
