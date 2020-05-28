// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Build", "CA1802:Field 'CorrelationIdSlot' is declared as 'readonly' but is initialized with a constant value. Mark this field as 'const' instead.", Justification = "<Pending>", Scope = "member", Target = "~F:NuGet.Common.ActivityCorrelationId.CorrelationIdSlot")]
[assembly: SuppressMessage("Build", "CA1823:Unused field 'AllowFipsAlgorithmsOnly'.", Justification = "<Pending>", Scope = "member", Target = "~F:NuGet.Common.CryptoHashUtility.AllowFipsAlgorithmsOnly")]
[assembly: SuppressMessage("Build", "CA1823:Unused field 'DotNetHome'.", Justification = "<Pending>", Scope = "member", Target = "~F:NuGet.Common.NuGetEnvironment.DotNetHome")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'AsyncLazy<T>.implicit operator Lazy<Task<T>>(AsyncLazy<T> outer)', validate parameter 'outer' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.AsyncLazy`1.op_Implicit(NuGet.Common.AsyncLazy`1)~System.Lazy{System.Threading.Tasks.Task{`0}}")]
[assembly: SuppressMessage("Build", "CA2225:Provide a method named 'ToLazy' or 'FromAsyncLazy' as an alternate for operator op_Implicit.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.AsyncLazy`1.op_Implicit(NuGet.Common.AsyncLazy`1)~System.Lazy{System.Threading.Tasks.Task{`0}}")]
[assembly: SuppressMessage("Build", "CA1305:The behavior of 'string.ToString()' could vary based on the current user's locale settings. Replace this call in 'ClientVersionUtility.GetNuGetAssemblyVersion()' with a call to 'string.ToString(IFormatProvider)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ClientVersionUtility.GetNuGetAssemblyVersion~System.String")]
[assembly: SuppressMessage("Build", "CA5350:FilePathToLockName uses a weak cryptographic algorithm SHA1", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ConcurrencyUtilities.FilePathToLockName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA5350:GetSha1HashProvider uses a weak cryptographic algorithm SHA1", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.CryptoHashUtility.GetSha1HashProvider~System.Security.Cryptography.HashAlgorithm")]
[assembly: SuppressMessage("Build", "CA1305:The behavior of 'string.Format(string, object, object)' could vary based on the current user's locale settings. Replace this call in 'DatetimeUtility.ToReadableTimeFormat(TimeSpan)' with a call to 'string.Format(IFormatProvider, string, params object[])'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.DatetimeUtility.ToReadableTimeFormat(System.TimeSpan)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'ExceptionLogger.ExceptionLogger(IEnvironmentVariableReader reader)', validate parameter 'reader' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ExceptionLogger.#ctor(NuGet.Common.IEnvironmentVariableReader)")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void ExceptionUtilities.LogException(Exception ex, ILogger logger, bool logStackAsError)', validate parameter 'logger' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ExceptionUtilities.LogException(System.Exception,NuGet.Common.ILogger,System.Boolean)")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'exception'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ExceptionUtilities.Unwrap(System.Exception)~System.Exception")]
[assembly: SuppressMessage("Build", "CA1308:In method 'GetTempFilePath', replace the call to 'ToLowerInvariant' with 'ToUpperInvariant'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.FileUtility.GetTempFilePath(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void LegacyLoggerAdapter.Log(ILogMessage message)', validate parameter 'message' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.LegacyLoggerAdapter.Log(NuGet.Common.ILogMessage)")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'Task LegacyLoggerAdapter.LogAsync(ILogMessage message)', validate parameter 'message' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.LegacyLoggerAdapter.LogAsync(NuGet.Common.ILogMessage)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'void LocalResourceUtils.DeleteDirectoryTree(string folderPath, List<string> failedDeletes)', validate parameter 'failedDeletes' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.LocalResourceUtils.DeleteDirectoryTree(System.String,System.Collections.Generic.List{System.String})")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string LoggingExtensions.FormatWithCode(ILogMessage message)', validate parameter 'message' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.LoggingExtensions.FormatWithCode(NuGet.Common.ILogMessage)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'IEnumerable<NuGetLogCode> MSBuildStringUtility.GetDistinctNuGetLogCodesOrDefault(IEnumerable<IEnumerable<NuGetLogCode>> nugetLogCodeLists)', validate parameter 'nugetLogCodeLists' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.MSBuildStringUtility.GetDistinctNuGetLogCodesOrDefault(System.Collections.Generic.IEnumerable{System.Collections.Generic.IEnumerable{NuGet.Common.NuGetLogCode}})~System.Collections.Generic.IEnumerable{NuGet.Common.NuGetLogCode}")]
[assembly: SuppressMessage("Build", "CA1827:Count() is used where Any() could be used instead to improve performance.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.MSBuildStringUtility.GetDistinctNuGetLogCodesOrDefault(System.Collections.Generic.IEnumerable{System.Collections.Generic.IEnumerable{NuGet.Common.NuGetLogCode}})~System.Collections.Generic.IEnumerable{NuGet.Common.NuGetLogCode}")]
[assembly: SuppressMessage("Build", "CA5364:Hard-coded use of deprecated security protocol Tls", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.NetworkProtocolUtility.ConfigureSupportedSslProtocols")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'IEnumerable<T> PathResolver.GetFilteredPackageFiles<T>(ICollection<T> source, Func<T, string> getPath, IEnumerable<string> wildcards)', validate parameter 'source' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathResolver.GetFilteredPackageFiles``1(System.Collections.Generic.ICollection{``0},System.Func{``0,System.String},System.Collections.Generic.IEnumerable{System.String})~System.Collections.Generic.IEnumerable{``0}")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'bool PathResolver.IsWildcardSearch(string filter)', validate parameter 'filter' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathResolver.IsWildcardSearch(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathResolver.NormalizeWildcardForExcludedFiles(string basePath, string wildcard)', validate parameter 'wildcard' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathResolver.NormalizeWildcardForExcludedFiles(System.String,System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1308:In method 'CheckCaseSenstivityRecursivelyTillDirectoryExists', replace the call to 'ToLowerInvariant' with 'ToUpperInvariant'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.CheckCaseSenstivityRecursivelyTillDirectoryExists(System.String,System.Boolean@)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'path'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.EnsureTrailingCharacter(System.String,System.Char)~System.String")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'path'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.EscapePSPath(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathUtility.GetDirectoryName(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.GetDirectoryName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'ZipArchiveEntry PathUtility.GetEntry(ZipArchive archive, string path)', validate parameter 'archive' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.GetEntry(System.IO.Compression.ZipArchive,System.String)~System.IO.Compression.ZipArchiveEntry")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathUtility.GetPath(Uri uri)', validate parameter 'uri' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.GetPath(System.Uri)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathUtility.GetPathWithBackSlashes(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.GetPathWithBackSlashes(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1303:Method 'string PathUtility.GetRelativePath(string path1, string path2, char separator)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message, string paramName)'. Retrieve the following string(s) from a resource table instead: \"Path must have a value\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.GetRelativePath(System.String,System.String,System.Char)~System.String")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'basePath'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.IsSubdirectory(System.String,System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathUtility.ReplaceAltDirSeparatorWithDirSeparator(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.ReplaceAltDirSeparatorWithDirSeparator(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'string PathUtility.ReplaceDirSeparatorWithAltDirSeparator(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.ReplaceDirSeparatorWithAltDirSeparator(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1507:Use nameof in place of string literal 'maxWidth'", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.SmartTruncate(System.String,System.Int32)~System.String")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.StartsWith(string)' could vary based on the current user's locale settings. Replace this call in 'NuGet.Common.PathUtility.StripLeadingDirectorySeparators(string)' with a call to 'string.StartsWith(string, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathUtility.StripLeadingDirectorySeparators(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.StartsWith(string)' could vary based on the current user's locale settings. Replace this call in 'NuGet.Common.PathValidator.IsValidLocalPath(string)' with a call to 'string.StartsWith(string, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathValidator.IsValidLocalPath(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'bool PathValidator.IsValidLocalPath(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathValidator.IsValidLocalPath(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'IsValidRelativePath' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathValidator.IsValidRelativePath(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1062:In externally visible method 'bool PathValidator.IsValidUncPath(string path)', validate parameter 'path' is non-null before using it. If appropriate, throw an ArgumentNullException when the argument is null or add a Code Contract precondition asserting non-null argument.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathValidator.IsValidUncPath(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1307:The behavior of 'string.StartsWith(string)' could vary based on the current user's locale settings. Replace this call in 'NuGet.Common.PathValidator.IsValidUncPath(string)' with a call to 'string.StartsWith(string, System.StringComparison)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.PathValidator.IsValidUncPath(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1303:Method 'string ProjectJsonPathUtilities.GetProjectConfigPath(string directoryPath, string projectName)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message)'. Retrieve the following string(s) from a resource table instead: \"projectName\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectConfigPath(System.String,System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA2208:Method GetProjectConfigPath passes parameter name 'projectName' as the message argument to a ArgumentException constructor. Replace this argument with a descriptive message and pass the parameter name in the correct position.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectConfigPath(System.String,System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA2208:Method GetProjectConfigWithProjectName passes parameter name 'projectName' as the message argument to a ArgumentException constructor. Replace this argument with a descriptive message and pass the parameter name in the correct position.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectConfigWithProjectName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1303:Method 'string ProjectJsonPathUtilities.GetProjectConfigWithProjectName(string projectName)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message)'. Retrieve the following string(s) from a resource table instead: \"projectName\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectConfigWithProjectName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA2208:Method GetProjectLockFileNameWithProjectName passes parameter name 'projectName' as the message argument to a ArgumentException constructor. Replace this argument with a descriptive message and pass the parameter name in the correct position.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectLockFileNameWithProjectName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1303:Method 'string ProjectJsonPathUtilities.GetProjectLockFileNameWithProjectName(string projectName)' passes a literal string as parameter 'message' of a call to 'ArgumentException.ArgumentException(string message)'. Retrieve the following string(s) from a resource table instead: \"projectName\".", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.GetProjectLockFileNameWithProjectName(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'IsProjectConfig' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.ProjectJsonPathUtilities.IsProjectConfig(System.String)~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1031:Modify 'GetIsMacOSX' to catch a more specific allowed exception type, or rethrow the exception.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.RuntimeEnvironmentHelper.GetIsMacOSX~System.Boolean")]
[assembly: SuppressMessage("Build", "CA1816:Change TelemetryActivity.Dispose() to call GC.SuppressFinalize(object). This will prevent derived types that introduce a finalizer from needing to re-implement 'IDisposable' to call it.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.TelemetryActivity.Dispose")]
[assembly: SuppressMessage("Build", "CA1305:The behavior of 'DateTime.ToString(string)' could vary based on the current user's locale settings. Replace this call in 'TelemetryActivity.Dispose()' with a call to 'DateTime.ToString(string, IFormatProvider)'.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.TelemetryActivity.Dispose")]
[assembly: SuppressMessage("Build", "CA1063:Modify 'TelemetryActivity.Dispose' so that it calls Dispose(true), then calls GC.SuppressFinalize on the current object instance ('this' or 'Me' in Visual Basic), and then returns.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.TelemetryActivity.Dispose")]
[assembly: SuppressMessage("Build", "CA1054:Change the type of parameter localOrUriPath of method UriUtility.GetLocalPath(string) from string to System.Uri, or provide an overload to UriUtility.GetLocalPath(string) that allows localOrUriPath to be passed as a System.Uri object.", Justification = "<Pending>", Scope = "member", Target = "~M:NuGet.Common.UriUtility.GetLocalPath(System.String)~System.String")]
[assembly: SuppressMessage("Build", "CA2237:Add [Serializable] to CommandLineArgumentCombinationException as this type implements ISerializable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.CommandLineArgumentCombinationException")]
[assembly: SuppressMessage("Build", "CA1052:Type 'CultureUtility' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.CultureUtility")]
[assembly: SuppressMessage("Build", "CA1052:Type 'DatetimeUtility' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.DatetimeUtility")]
[assembly: SuppressMessage("Build", "CA1052:Type 'LocalResourceUtils' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.LocalResourceUtils")]
[assembly: SuppressMessage("Build", "CA1012:Abstract type LoggerBase should not have constructors", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.LoggerBase")]
[assembly: SuppressMessage("Build", "CA1052:Type 'LogMessageProperties' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.LogMessageProperties")]
[assembly: SuppressMessage("Build", "CA1815:SearchPathResult should override Equals.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.PathResolver.SearchPathResult")]
[assembly: SuppressMessage("Build", "CA1052:Type 'Preprocessor' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.Preprocessor")]
[assembly: SuppressMessage("Build", "CA1052:Type 'ProjectJsonPathUtilities' is a static holder type but is neither static nor NotInheritable", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.ProjectJsonPathUtilities")]
[assembly: SuppressMessage("Build", "CA1063:Provide an overridable implementation of Dispose(bool) on 'TelemetryActivity' or mark the type as sealed. A call to Dispose(false) should only clean up native resources. A call to Dispose(true) should clean up both managed and native resources.", Justification = "<Pending>", Scope = "type", Target = "~T:NuGet.Common.TelemetryActivity")]
