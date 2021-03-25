// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1017:MarkAssembliesWithComVisible")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NuGetConsole.Implementation")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NuGetConsole.Implementation.Console")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NuGetConsole.Implementation.PowerConsole")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NuGetConsole.DebugConsole")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "NuGetConsole.Host.PowerShell")]
[assembly: SuppressMessage("Usage", "VSTHRD110:Observe result of async calls", Justification = "https://github.com/NuGet/Home/issues/7674", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.ConsoleDispatcher.Start")]
[assembly: SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = "Dispose method", Scope = "member", Target = "~M:NuGetConsole.ChannelOutputConsole.Dispose(System.Boolean)")]
[assembly: SuppressMessage("Build", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>", Scope = "member", Target = "~F:NuGetConsole.GuidList.guidNuGetOutputWindowPaneGuid")]
[assembly: SuppressMessage("Build", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>", Scope = "member", Target = "~F:NuGetConsole.GuidList.guidVsWindowKindOutput")]
[assembly: SuppressMessage("Build", "CA2213:'WpfConsole' contains field '_marshaler' that is of IDisposable type 'PrivateMarshaler', but it is never disposed. Change the Dispose method on 'WpfConsole' to call Close or Dispose on this field.", Justification = "The Dispose() method on _marshaler is called when the tool window is closed.", Scope = "member", Target = "~F:NuGetConsole.Implementation.Console.WpfConsole._marshaler")]
[assembly: SuppressMessage("Build", "CA2213:'PowerConsoleWindow' contains field '_activeHostInfo' that is of IDisposable type 'HostInfo', but it is never disposed. Change the Dispose method on 'PowerConsoleWindow' to call Close or Dispose on this field.", Justification = " '_activeHostInfo' field holds reference to the first item in '_hostInfos' collection whose items are disposed.", Scope = "member", Target = "~F:NuGetConsole.Implementation.PowerConsole.PowerConsoleWindow._activeHostInfo")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'CloseChannelAsync' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.ChannelOutputConsole.CloseChannelAsync~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'Dispose' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.ChannelOutputConsole.Dispose(System.Boolean)")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'T CommonExtensionMethods.GetService<T>(IServiceProvider sp, Type serviceType)', validate parameter 'sp' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.CommonExtensionMethods.GetService``1(System.IServiceProvider,System.Type)~``0")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'displayName'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.DisplayNameAttribute.#ctor(System.String)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'hostName'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.HostNameAttribute.#ctor(System.String)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'key'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.ConsoleDispatcher.PostKey(NuGet.VisualStudio.VsKeyInfo)")]
[assembly: SuppressMessage("Build", "CA1303:Method 'void ConsoleDispatcher.Start()' passes a literal string as parameter 'message' of a call to 'InvalidOperationException.InvalidOperationException(string message)'. Retrieve the following string(s) from a resource table instead: \"Can't start Console dispatcher. Host is null.\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.ConsoleDispatcher.Start")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'operation'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.WpfConsole.WriteProgress(System.String,System.Int32)")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'GetTabExpansionTimeout' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.WpfConsoleKeyProcessor.GetTabExpansionTimeout~System.Int32")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.Equals(string, string)' could vary based on the current user's locale settings. Replace this call in 'NuGetConsole.Implementation.Console.WpfConsoleService.GetSingletonHostService<IService, IServiceFactory>(NuGetConsole.Implementation.Console.WpfConsole, System.Collections.Generic.IEnumerable<System.Lazy<IServiceFactory, NuGetConsole.Implementation.Console.IHostNameMetadata>>, System.Func<IServiceFactory, NuGet.VisualStudio.IHost, IService>, System.Func<IService>)' with a call to 'string.Equals(string, string, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.Console.WpfConsoleService.GetSingletonHostService``2(NuGetConsole.Implementation.Console.WpfConsole,System.Collections.Generic.IEnumerable{System.Lazy{``1,NuGetConsole.Implementation.Console.IHostNameMetadata}},System.Func{``1,NuGet.VisualStudio.IHost,``0},System.Func{``0})~``0")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'LoadConsoleEditorAsync' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.PowerConsoleToolWindow.LoadConsoleEditorAsync~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Build", "CA1303:Method 'void PowerConsoleToolWindow.ProjectsList_Exec(object sender, EventArgs e)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message, string paramName)'. Retrieve the following string(s) from a resource table instead: \"Invalid argument\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.PowerConsoleToolWindow.ProjectsList_Exec(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'e'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.PowerConsoleToolWindow.ProjectsList_Exec(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Build", "CA1303:Method 'void PowerConsoleToolWindow.SourcesList_Exec(object sender, EventArgs e)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message, string paramName)'. Retrieve the following string(s) from a resource table instead: \"Invalid argument\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.PowerConsoleToolWindow.SourcesList_Exec(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'e'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.Implementation.PowerConsoleToolWindow.SourcesList_Exec(System.Object,System.EventArgs)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'arg'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.UtilityMethods.ThrowIfArgumentNull``1(``0)")]
[assembly: SuppressMessage("Build", "CA1303:Method 'void UtilityMethods.ThrowIfArgumentNullOrEmpty(string arg)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message, string paramName)'. Retrieve the following string(s) from a resource table instead: \"Invalid argument\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.UtilityMethods.ThrowIfArgumentNullOrEmpty(System.String)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'arg'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGetConsole.UtilityMethods.ThrowIfArgumentNullOrEmpty(System.String)")]
[assembly: SuppressMessage("Build", "CA1806:get_VsTextView creates a new instance of WpfConsoleKeyProcessor which is never used. Pass the instance as an argument to another method, assign the instance to a variable, or remove the object creation if it is unnecessary.", Justification = "<Pending>", Scope = "member", Target = "~P:NuGetConsole.Implementation.Console.WpfConsole.VsTextView")]
[assembly: SuppressMessage("Build", "CA1501:'ConsoleContainer' has an object hierarchy '9' levels deep within the defining module. If possible, eliminate base classes within the hierarchy to decrease its hierarchy level below '6': 'UserControl, ContentControl, Control, FrameworkElement, UIElement, Visual, DependencyObject, DispatcherObject, Object'", Justification = "<Pending>", Scope = "type", Target = "~T:NuGetConsole.ConsoleContainer")]
