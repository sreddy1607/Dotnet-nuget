﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NuGet.Protocol.Core.v3 {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Strings() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NuGet.Protocol.Core.v3.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Install failed. Rolling back....
        /// </summary>
        internal static string ActionExecutor_RollingBack {
            get {
                return ResourceManager.GetString("ActionExecutor_RollingBack", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unrecognized Package Action &apos;{0}&apos;..
        /// </summary>
        internal static string ActionResolver_UnsupportedAction {
            get {
                return ResourceManager.GetString("ActionResolver_UnsupportedAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unsupported Dependency Behavior &apos;{0}&apos;..
        /// </summary>
        internal static string ActionResolver_UnsupportedDependencyBehavior {
            get {
                return ResourceManager.GetString("ActionResolver_UnsupportedDependencyBehavior", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package &apos;{0}&apos; already exists at feed &apos;{1}&apos; and is invalid..
        /// </summary>
        internal static string AddPackage_ExistingPackageInvalid {
            get {
                return ResourceManager.GetString("AddPackage_ExistingPackageInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Package &apos;{0}&apos; already exists at feed &apos;{1}&apos;..
        /// </summary>
        internal static string AddPackage_PackageAlreadyExists {
            get {
                return ResourceManager.GetString("AddPackage_PackageAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Successfully added package &apos;{0}&apos; to feed &apos;{1}&apos;..
        /// </summary>
        internal static string AddPackage_SuccessfullyAdded {
            get {
                return ResourceManager.GetString("AddPackage_SuccessfullyAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Argument can not be null or empty..
        /// </summary>
        internal static string Argument_Cannot_Be_Null_Or_Empty {
            get {
                return ResourceManager.GetString("Argument_Cannot_Be_Null_Or_Empty", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Argument cannot be null or empty..
        /// </summary>
        internal static string ArgumentCannotBeNullOrEmpty {
            get {
                return ResourceManager.GetString("ArgumentCannotBeNullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to the symbol server.
        /// </summary>
        internal static string DefaultSymbolServer {
            get {
                return ResourceManager.GetString("DefaultSymbolServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Delete canceled.
        /// </summary>
        internal static string DeleteCommandCanceled {
            get {
                return ResourceManager.GetString("DeleteCommandCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to {0} {1} will be deleted from the {2}. Would you like to continue?.
        /// </summary>
        internal static string DeleteCommandConfirm {
            get {
                return ResourceManager.GetString("DeleteCommandConfirm", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to {0} {1} was deleted successfully..
        /// </summary>
        internal static string DeleteCommandDeletedPackage {
            get {
                return ResourceManager.GetString("DeleteCommandDeletedPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Deleting {0} {1} from the {2}..
        /// </summary>
        internal static string DeleteCommandDeletingPackage {
            get {
                return ResourceManager.GetString("DeleteCommandDeletingPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Not Found..
        /// </summary>
        internal static string DeletePackage_NotFound {
            get {
                return ResourceManager.GetString("DeletePackage_NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The download URL for {0} &apos;{1}&apos; is invalid..
        /// </summary>
        internal static string DownloadActionHandler_InvalidDownloadUrl {
            get {
                return ResourceManager.GetString("DownloadActionHandler_InvalidDownloadUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No download URL could be found for {0}..
        /// </summary>
        internal static string DownloadActionHandler_NoDownloadUrl {
            get {
                return ResourceManager.GetString("DownloadActionHandler_NoDownloadUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The download of &apos;{0}&apos; timed out because no data was received for {1}ms..
        /// </summary>
        internal static string Error_DownloadTimeout {
            get {
                return ResourceManager.GetString("Error_DownloadTimeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to {0} {1}.
        /// </summary>
        internal static string Http_RequestLog {
            get {
                return ResourceManager.GetString("Http_RequestLog", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to {0} {1} {2}ms.
        /// </summary>
        internal static string Http_ResponseLog {
            get {
                return ResourceManager.GetString("Http_ResponseLog", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The HTTP request to &apos;{0} {1}&apos; has timed out after {2}ms..
        /// </summary>
        internal static string Http_Timeout {
            get {
                return ResourceManager.GetString("Http_Timeout", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The folder &apos;{0}&apos; contains an invalid version..
        /// </summary>
        internal static string InvalidVersionFolder {
            get {
                return ResourceManager.GetString("InvalidVersionFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to the NuGet gallery.
        /// </summary>
        internal static string LiveFeed {
            get {
                return ResourceManager.GetString("LiveFeed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Downloading a package from &apos;{0}&apos; was canceled..
        /// </summary>
        internal static string Log_CanceledNupkgDownload {
            get {
                return ResourceManager.GetString("Log_CanceledNupkgDownload", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Error downloading &apos;{0}&apos; from &apos;{1}&apos;..
        /// </summary>
        internal static string Log_ErrorDownloading {
            get {
                return ResourceManager.GetString("Log_ErrorDownloading", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Failed to download package from &apos;{0}&apos;..
        /// </summary>
        internal static string Log_FailedToDownloadPackage {
            get {
                return ResourceManager.GetString("Log_FailedToDownloadPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The feed at &apos;{0}&apos; returned an unexpected status code &apos;{1} {2}&apos;..
        /// </summary>
        internal static string Log_FailedToFetchFeed {
            get {
                return ResourceManager.GetString("Log_FailedToFetchFeed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unable to load package &apos;{0}&apos;..
        /// </summary>
        internal static string Log_FailedToGetNupkgStream {
            get {
                return ResourceManager.GetString("Log_FailedToGetNupkgStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unable to load nuspec from package &apos;{0}&apos;..
        /// </summary>
        internal static string Log_FailedToGetNuspecStream {
            get {
                return ResourceManager.GetString("Log_FailedToGetNuspecStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unable to load the service index for source {0}..
        /// </summary>
        internal static string Log_FailedToReadServiceIndex {
            get {
                return ResourceManager.GetString("Log_FailedToReadServiceIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Failed to retrieve information from remote source &apos;{0}&apos;..
        /// </summary>
        internal static string Log_FailedToRetrievePackage {
            get {
                return ResourceManager.GetString("Log_FailedToRetrievePackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The file &apos;{0}&apos; is corrupt..
        /// </summary>
        internal static string Log_FileIsCorrupt {
            get {
                return ResourceManager.GetString("Log_FileIsCorrupt", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to An invalid cache entry was found for URL &apos;{0}&apos; and will be replaced..
        /// </summary>
        internal static string Log_InvalidCacheEntry {
            get {
                return ResourceManager.GetString("Log_InvalidCacheEntry", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The nupkg at &apos;{0}&apos; is not valid..
        /// </summary>
        internal static string Log_InvalidNupkgFromUrl {
            get {
                return ResourceManager.GetString("Log_InvalidNupkgFromUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Retrying &apos;{0}&apos; for source &apos;{1}&apos;..
        /// </summary>
        internal static string Log_RetryingFindPackagesById {
            get {
                return ResourceManager.GetString("Log_RetryingFindPackagesById", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to An error was encountered when fetching &apos;{0} {1}&apos;. The request will now be retried..
        /// </summary>
        internal static string Log_RetryingHttp {
            get {
                return ResourceManager.GetString("Log_RetryingHttp", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Retrying service index request for source &apos;{0}&apos;..
        /// </summary>
        internal static string Log_RetryingServiceIndex {
            get {
                return ResourceManager.GetString("Log_RetryingServiceIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No API Key was provided and no API Key could be found for {0}. To save an API Key for a source use the &apos;setApiKey&apos; command..
        /// </summary>
        internal static string NoApiKeyFound {
            get {
                return ResourceManager.GetString("NoApiKeyFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Cannot create a NuGet Repository from the Aggregate Source.
        /// </summary>
        internal static string NuGetRepository_CannotCreateAggregateRepo {
            get {
                return ResourceManager.GetString("NuGetRepository_CannotCreateAggregateRepo", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The {0} service is not supported by this object..
        /// </summary>
        internal static string NuGetServiceProvider_ServiceNotSupported {
            get {
                return ResourceManager.GetString("NuGetServiceProvider_ServiceNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &apos;{0}&apos; is not a valid nupkg file..
        /// </summary>
        internal static string NupkgPath_Invalid {
            get {
                return ResourceManager.GetString("NupkgPath_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to One or more URIs must be specified..
        /// </summary>
        internal static string OneOrMoreUrisMustBeSpecified {
            get {
                return ResourceManager.GetString("OneOrMoreUrisMustBeSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Unrecognized Package Action &apos;{0}&apos;..
        /// </summary>
        internal static string PackageActionDescriptionWrapper_UnrecognizedAction {
            get {
                return ResourceManager.GetString("PackageActionDescriptionWrapper_UnrecognizedAction", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to ERROR: This version of nuget.exe does not support updating packages to package source &apos;{0}&apos;..
        /// </summary>
        internal static string PackageServerEndpoint_NotSupported {
            get {
                return ResourceManager.GetString("PackageServerEndpoint_NotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &apos;{0}&apos; is not a valid path..
        /// </summary>
        internal static string Path_Invalid {
            get {
                return ResourceManager.GetString("Path_Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &apos;{0}&apos; should be a local path or a UNC share path..
        /// </summary>
        internal static string Path_Invalid_NotFileNotUnc {
            get {
                return ResourceManager.GetString("Path_Invalid_NotFileNotUnc", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The project &apos;{0}&apos; is not one of the projects targetted by this object..
        /// </summary>
        internal static string ProjectInstallationTarget_ProjectIsNotTargetted {
            get {
                return ResourceManager.GetString("ProjectInstallationTarget_ProjectIsNotTargetted", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Failed to retrieve metadata from source &apos;{0}&apos;..
        /// </summary>
        internal static string Protocol_BadSource {
            get {
                return ResourceManager.GetString("Protocol_BadSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The &apos;versions&apos; property at &apos;{0}&apos; must be an array..
        /// </summary>
        internal static string Protocol_FlatContainerIndexVersionsNotArray {
            get {
                return ResourceManager.GetString("Protocol_FlatContainerIndexVersionsNotArray", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Service index document is missing the &apos;resources&apos; property..
        /// </summary>
        internal static string Protocol_IndexMissingResourcesNode {
            get {
                return ResourceManager.GetString("Protocol_IndexMissingResourcesNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The content at &apos;{0}&apos; is not a valid JSON object..
        /// </summary>
        internal static string Protocol_InvalidJsonObject {
            get {
                return ResourceManager.GetString("Protocol_InvalidJsonObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The source does not have the &apos;version&apos; property at &apos;{0}&apos;..
        /// </summary>
        internal static string Protocol_InvalidServiceIndex {
            get {
                return ResourceManager.GetString("Protocol_InvalidServiceIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The content at &apos;{0}&apos; is not valid XML..
        /// </summary>
        internal static string Protocol_InvalidXml {
            get {
                return ResourceManager.GetString("Protocol_InvalidXml", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Metadata could not be loaded from the source &apos;{0}&apos;..
        /// </summary>
        internal static string Protocol_MalformedMetadataError {
            get {
                return ResourceManager.GetString("Protocol_MalformedMetadataError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The source does not have a Registration Base Url defined!.
        /// </summary>
        internal static string Protocol_MissingRegistrationBase {
            get {
                return ResourceManager.GetString("Protocol_MissingRegistrationBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The source does not have a Search service!.
        /// </summary>
        internal static string Protocol_MissingSearchService {
            get {
                return ResourceManager.GetString("Protocol_MissingSearchService", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The source does not have the &apos;version&apos; property..
        /// </summary>
        internal static string Protocol_MissingVersion {
            get {
                return ResourceManager.GetString("Protocol_MissingVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to An error occurred while retrieving package metadata for &apos;{0}&apos; from source &apos;{1}&apos;..
        /// </summary>
        internal static string Protocol_PackageMetadataError {
            get {
                return ResourceManager.GetString("Protocol_PackageMetadataError", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The source version is not supported: &apos;{0}&apos;..
        /// </summary>
        internal static string Protocol_UnsupportedVersion {
            get {
                return ResourceManager.GetString("Protocol_UnsupportedVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Your package was pushed..
        /// </summary>
        internal static string PushCommandPackagePushed {
            get {
                return ResourceManager.GetString("PushCommandPackagePushed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Pushing {0} to {1}....
        /// </summary>
        internal static string PushCommandPushingPackage {
            get {
                return ResourceManager.GetString("PushCommandPushingPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The &apos;{0}&apos; installation feature was required by a package but is not supported on the current host..
        /// </summary>
        internal static string RequiredFeatureUnsupportedException_DefaultMessageWithFeature {
            get {
                return ResourceManager.GetString("RequiredFeatureUnsupportedException_DefaultMessageWithFeature", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The installation host does not support a feature required by this package..
        /// </summary>
        internal static string RequiredFeatureUnsupportedException_DefaultMessageWithoutFeature {
            get {
                return ResourceManager.GetString("RequiredFeatureUnsupportedException_DefaultMessageWithoutFeature", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to File does not exist ({0})..
        /// </summary>
        internal static string UnableToFindFile {
            get {
                return ResourceManager.GetString("UnableToFindFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Found symbols package &apos;{0}&apos;, but no API key was specified for the symbol server. To save an API Key, run &apos;NuGet.exe setApiKey [your API key from http://www.NuGet.org]&apos;..
        /// </summary>
        internal static string Warning_SymbolServerNotConfigured {
            get {
                return ResourceManager.GetString("Warning_SymbolServerNotConfigured", resourceCulture);
            }
        }
    }
}
