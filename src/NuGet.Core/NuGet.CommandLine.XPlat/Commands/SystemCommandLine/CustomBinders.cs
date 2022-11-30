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
    internal partial class AddSourceCustomBinder : BinderBase<AddSourceArgs>
    {
        private readonly Argument<string> _source;
        private readonly Option<string> _name;
        private readonly Option<string> _username;
        private readonly Option<string> _password;
        private readonly Option<bool> _storePasswordInClearText;
        private readonly Option<string> _validAuthenticationTypes;
        private readonly Option<string> _configfile;

        public AddSourceCustomBinder(Argument<string> source, Option<string> name, Option<string> username, Option<string> password, Option<bool> storePasswordInClearText, Option<string> validAuthenticationTypes, Option<string> configfile)
        {
            _source = source;
            _name = name;
            _username = username;
            _password = password;
            _storePasswordInClearText = storePasswordInClearText;
            _validAuthenticationTypes = validAuthenticationTypes;
            _configfile = configfile;
        }

        protected override AddSourceArgs GetBoundValue(BindingContext bindingContext)
        {
            var returnValue = new AddSourceArgs()
            {
                Source = bindingContext.ParseResult.GetValueForArgument(_source),
                Name = bindingContext.ParseResult.GetValueForOption(_name),
                Username = bindingContext.ParseResult.GetValueForOption(_username),
                Password = bindingContext.ParseResult.GetValueForOption(_password),
                StorePasswordInClearText = bindingContext.ParseResult.GetValueForOption(_storePasswordInClearText),
                ValidAuthenticationTypes = bindingContext.ParseResult.GetValueForOption(_validAuthenticationTypes),
                Configfile = bindingContext.ParseResult.GetValueForOption(_configfile),
            };
            return returnValue;
        } // end GetBoundValue method
    } // end class

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