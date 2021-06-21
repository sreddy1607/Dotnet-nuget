// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Build", "CA1303:Method 'int SemanticVersion.CompareTo(object obj)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message)'. Retrieve the following string(s) from a resource table instead: \"obj\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.SemanticVersion.CompareTo(System.Object)~System.Int32")]
[assembly: SuppressMessage("Build", "CA1303:Method 'SemanticVersion SemanticVersion.Parse(string version)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message, string paramName)'. Retrieve the following string(s) from a resource table instead: \"Value cannot be null or an empty string.\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.SemanticVersion.Parse(System.String)~NuGet.SemanticVersion")]
[assembly: SuppressMessage("Build", "CA1806:ParseOptionalVersion calls TryParse but does not explicitly check whether the conversion succeeded. Either use the return value in a conditional statement or verify that the call site expects that the out argument will be set to the default value when the conversion fails.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.SemanticVersion.ParseOptionalVersion(System.String)~NuGet.SemanticVersion")]
[assembly: SuppressMessage("Build", "CA1010:Collection 'IVsProjectProperties' directly or indirectly inherits 'IEnumerable' without implementing 'IEnumerable<T>'. Publicly-visible collections should implement the generic version to broaden usability.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.VisualStudio.IVsProjectProperties")]
[assembly: SuppressMessage("Build", "CA1010:Collection 'IVsReferenceItems' directly or indirectly inherits 'IEnumerable' without implementing 'IEnumerable<T>'. Publicly-visible collections should implement the generic version to broaden usability.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.VisualStudio.IVsReferenceItems")]
[assembly: SuppressMessage("Build", "CA1010:Collection 'IVsReferenceProperties' directly or indirectly inherits 'IEnumerable' without implementing 'IEnumerable<T>'. Publicly-visible collections should implement the generic version to broaden usability.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.VisualStudio.IVsReferenceProperties")]
[assembly: SuppressMessage("Build", "CA1010:Collection 'IVsTargetFrameworks' directly or indirectly inherits 'IEnumerable' without implementing 'IEnumerable<T>'. Publicly-visible collections should implement the generic version to broaden usability.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.VisualStudio.IVsTargetFrameworks")]
[assembly: SuppressMessage("Build", "CA1010:Collection 'IVsTargetFrameworks2' directly or indirectly inherits 'IEnumerable' without implementing 'IEnumerable<T>'. Publicly-visible collections should implement the generic version to broaden usability.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.VisualStudio.IVsTargetFrameworks2")]
