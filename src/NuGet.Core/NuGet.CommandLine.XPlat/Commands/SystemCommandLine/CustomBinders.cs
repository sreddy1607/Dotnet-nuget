// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// Do not manually edit this autogenerated file:
// instead modify the neighboring .tt file (text template) and/or NuGet.CommandLine.Xplat\Commands\SystemCommandLine\Commands.xml (data file),
// then re-execute the text template via "run custom tool" on VS context menu for .tt file, or via dotnet-t4 global tool.

using System.CommandLine;
using System.CommandLine.Binding;
using NuGet.Commands;

namespace NuGet.CommandLine.XPlat.Commands
{
    internal partial class ListSourceCustomBinder : BinderBase<ListSourceArgs>
    {
        private readonly Option<string> _format;
        private readonly Option<string> _configfile;

        public ListSourceCustomBinder(Option<string> format, Option<string> configfile)
        {
            _format = format;
            _configfile = configfile;
        }

        protected override ListSourceArgs GetBoundValue(BindingContext bindingContext)
        {
            var returnValue = new ListSourceArgs()
            {
                Format = bindingContext.ParseResult.GetValueForOption(_format),
                Configfile = bindingContext.ParseResult.GetValueForOption(_configfile),
            };
            return returnValue;
        } // end GetBoundValue method
    } // end class

} // end namespace
