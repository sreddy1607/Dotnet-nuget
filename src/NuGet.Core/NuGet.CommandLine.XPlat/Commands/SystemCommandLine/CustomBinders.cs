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

    internal partial class PushCustomBinder : BinderBase<PushArgs>
    {
        private readonly Option<bool> _forceEnglishOutput;
        private readonly Option<string> _source;
        private readonly Option<string> _symbolSource;
        private readonly Option<int> _timeout;
        private readonly Option<string> _apiKey;
        private readonly Option<string> _symbolApiKey;
        private readonly Option<bool> _disableBuffering;
        private readonly Option<bool> _noSymbols;
        private readonly Argument<string> _packagePaths;
        private readonly Option<bool> _noServiceEndpoint;
        private readonly Option<bool> _interactive;
        private readonly Option<bool> _skipDuplicate;

        public PushCustomBinder(Option<bool> forceEnglishOutput, Option<string> source, Option<string> symbolSource, Option<int> timeout, Option<string> apiKey, Option<string> symbolApiKey, Option<bool> disableBuffering, Option<bool> noSymbols, Argument<string> packagePaths, Option<bool> noServiceEndpoint, Option<bool> interactive, Option<bool> skipDuplicate)
        {
            _forceEnglishOutput = forceEnglishOutput;
            _source = source;
            _symbolSource = symbolSource;
            _timeout = timeout;
            _apiKey = apiKey;
            _symbolApiKey = symbolApiKey;
            _disableBuffering = disableBuffering;
            _noSymbols = noSymbols;
            _packagePaths = packagePaths;
            _noServiceEndpoint = noServiceEndpoint;
            _interactive = interactive;
            _skipDuplicate = skipDuplicate;
        }

        protected override PushArgs GetBoundValue(BindingContext bindingContext)
        {
            var returnValue = new PushArgs()
            {
                ForceEnglishOutput = bindingContext.ParseResult.GetValueForOption(_forceEnglishOutput),
                Source = bindingContext.ParseResult.GetValueForOption(_source),
                SymbolSource = bindingContext.ParseResult.GetValueForOption(_symbolSource),
                Timeout = bindingContext.ParseResult.GetValueForOption(_timeout),
                ApiKey = bindingContext.ParseResult.GetValueForOption(_apiKey),
                SymbolApiKey = bindingContext.ParseResult.GetValueForOption(_symbolApiKey),
                DisableBuffering = bindingContext.ParseResult.GetValueForOption(_disableBuffering),
                NoSymbols = bindingContext.ParseResult.GetValueForOption(_noSymbols),
                PackagePaths = bindingContext.ParseResult.GetValueForArgument(_packagePaths),
                NoServiceEndpoint = bindingContext.ParseResult.GetValueForOption(_noServiceEndpoint),
                Interactive = bindingContext.ParseResult.GetValueForOption(_interactive),
                SkipDuplicate = bindingContext.ParseResult.GetValueForOption(_skipDuplicate),
            };
            return returnValue;
        } // end GetBoundValue method
    } // end class

} // end namespace
