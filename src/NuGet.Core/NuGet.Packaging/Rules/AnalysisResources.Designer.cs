﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NuGet.Packaging.Rules {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AnalysisResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AnalysisResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NuGet.Packaging.Rules.AnalysisResources", typeof(AnalysisResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The assembly &apos;{0}&apos; is placed directly under &apos;lib&apos; folder. It is recommended that assemblies be placed inside a framework-specific folder. Move it into a framework-specific folder..
        /// </summary>
        public static string AssemblyDirectlyUnderLibWarning {
            get {
                return ResourceManager.GetString("AssemblyDirectlyUnderLibWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The assembly &apos;{0}&apos; is not inside the &apos;lib&apos; folder and hence it won&apos;t be added as a reference when the package is installed into a project. Move it into the &apos;lib&apos; folder if it needs to be referenced..
        /// </summary>
        public static string AssemblyOutsideLibWarning {
            get {
                return ResourceManager.GetString("AssemblyOutsideLibWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - At least one {0} file was found in &apos;{1}&apos;, but &apos;{2}&apos; was not..
        /// </summary>
        public static string BuildConventionIsViolatedWarning {
            get {
                return ResourceManager.GetString("BuildConventionIsViolatedWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &quot;{0}&quot; for {1} is a sample value and should be removed. Replace it with an appropriate value or remove it and rebuild your package..
        /// </summary>
        public static string DefaultSpecValueWarning {
            get {
                return ResourceManager.GetString("DefaultSpecValueWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Add lib or ref assemblies for the.
        /// </summary>
        public static string DependenciesGroupsForEachTFMBeginningToFiles {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMBeginningToFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Add a dependency group for.
        /// </summary>
        public static string DependenciesGroupsForEachTFMBeginningToNuspec {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMBeginningToNuspec", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to target framework.
        /// </summary>
        public static string DependenciesGroupsForEachTFMEndingToFile {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMEndingToFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to to the nuspec.
        /// </summary>
        public static string DependenciesGroupsForEachTFMEndingToNuspec {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMEndingToNuspec", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some target frameworks declared in the dependencies group of the nuspec and the lib/ref folder have compatible matches, but not exact matches in the other location. Unless intentional, consult the list of actions below:.
        /// </summary>
        public static string DependenciesGroupsForEachTFMHasCompatMatch {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMHasCompatMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Some target frameworks declared in the dependencies group of the nuspec and the lib/ref folder do not have exact matches in the other location. Consult the list of actions below:.
        /// </summary>
        public static string DependenciesGroupsForEachTFMHasNoExactMatch {
            get {
                return ResourceManager.GetString("DependenciesGroupsForEachTFMHasNoExactMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file &apos;{0}&apos; path, name, or both are too long. Your package might not work without long file path support. Please shorten the file path or file name..
        /// </summary>
        public static string FilePathTooLongWarning {
            get {
                return ResourceManager.GetString("FilePathTooLongWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;PackageIconUrl&apos;/&apos;iconUrl&apos; element is deprecated. Consider using the &apos;PackageIcon&apos;/&apos;icon&apos; element instead..
        /// </summary>
        public static string IconUrlDeprecationWarning {
            get {
                return ResourceManager.GetString("IconUrlDeprecationWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The folder &apos;{0}&apos; under &apos;lib&apos; is not recognized as a valid framework name or a supported culture identifier. Rename it to a valid framework name or culture identifier..
        /// </summary>
        public static string InvalidFrameworkWarning {
            get {
                return ResourceManager.GetString("InvalidFrameworkWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file at &apos;{0}&apos; uses the symbol for empty directory &apos;_._&apos;, but it is present in a directory that contains other files. Please remove this file from directories that contain other files..
        /// </summary>
        public static string InvalidPlaceholderFileWarning {
            get {
                return ResourceManager.GetString("InvalidPlaceholderFileWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A stable release of a package should not have a prerelease dependency. Either modify the version spec of dependency &quot;{0}&quot; or update the version field in the nuspec..
        /// </summary>
        public static string InvalidPrereleaseDependencyWarning {
            get {
                return ResourceManager.GetString("InvalidPrereleaseDependencyWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The package version &apos;{0}&apos; uses SemVer 2.0.0 or components of SemVer 1.0.0 that are not supported on legacy clients. Change the package version to a SemVer 1.0.0 string. If the version contains a release label it must start with a letter. This message can be ignored if the package is not intended for older clients..
        /// </summary>
        public static string LegacyVersionWarning {
            get {
                return ResourceManager.GetString("LegacyVersionWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;licenseUrl&apos; element will be deprecated. Consider using the &apos;license&apos; element instead..
        /// </summary>
        public static string LicenseUrlDeprecationWarning {
            get {
                return ResourceManager.GetString("LicenseUrlDeprecationWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The assembly &apos;lib\{0}&apos; will be ignored when the package is installed after the migration..
        /// </summary>
        public static string Migrator_AssemblyDirectlyUnderLibWarning {
            get {
                return ResourceManager.GetString("Migrator_AssemblyDirectlyUnderLibWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;content&apos; assets will not be available when the package is installed after the migration..
        /// </summary>
        public static string Migrator_PackageHasContentFolder {
            get {
                return ResourceManager.GetString("Migrator_PackageHasContentFolder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to install.ps1 script will be ignored when the package is installed after the migration..
        /// </summary>
        public static string Migrator_PackageHasInstallScript {
            get {
                return ResourceManager.GetString("Migrator_PackageHasInstallScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to XDT transform file &apos;{0}&apos; will not be applied when the package is installed after the migration..
        /// </summary>
        public static string Migrator_XdtTransformInPackage {
            get {
                return ResourceManager.GetString("Migrator_XdtTransformInPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file &apos;{0}&apos; will be ignored by NuGet because it is not directly under &apos;tools&apos; folder. Place the file directly under &apos;tools&apos; folder..
        /// </summary>
        public static string MisplacedInitScriptWarning {
            get {
                return ResourceManager.GetString("MisplacedInitScriptWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The transform file &apos;{0}&apos; is outside the &apos;content&apos; folder and hence will not be transformed during installation of this package. Move it into the &apos;content&apos; folder..
        /// </summary>
        public static string MisplacedTransformFileWarning {
            get {
                return ResourceManager.GetString("MisplacedTransformFileWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This package does not contain a lib/ or ref/ folder, and will therefore be treated as compatible for all frameworks. Since framework specific files were found under the build/ directory for {0}, consider creating the following empty files to correctly narrow the compatibility of the package:
        ///{1}.
        /// </summary>
        public static string NoRefOrLibFolderInPackage {
            get {
                return ResourceManager.GetString("NoRefOrLibFolderInPackage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An empty folder placeholder file &apos;{0}&apos; is in a non-empty folder and should be removed..
        /// </summary>
        public static string PlaceholderFileInPackageWarning {
            get {
                return ResourceManager.GetString("PlaceholderFileInPackageWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to References were found in the nuspec, but some reference assemblies were not found in both the nuspec and ref folder. Add the following reference assemblies:.
        /// </summary>
        public static string ReferencesInNuspecAndRefFilesDontMatchWarning {
            get {
                return ResourceManager.GetString("ReferencesInNuspecAndRefFilesDontMatchWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Add {0} to the {1} reference group in the nuspec.
        /// </summary>
        public static string ReferencesInNuspecAndRefFilesDontMatchWarningAddToNuspecListItemFormat {
            get {
                return ResourceManager.GetString("ReferencesInNuspecAndRefFilesDontMatchWarningAddToNuspecListItemFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Add {0} to the references element in the nuspec.
        /// </summary>
        public static string ReferencesInNuspecAndRefFilesDontMatchWarningAddToNuspecNoTfmListItemFormat {
            get {
                return ResourceManager.GetString("ReferencesInNuspecAndRefFilesDontMatchWarningAddToNuspecNoTfmListItemFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to - Add {0} to the ref/{1}/ directory.
        /// </summary>
        public static string ReferencesInNuspecAndRefFilesDontMatchWarningAddToRefListItemFormat {
            get {
                return ResourceManager.GetString("ReferencesInNuspecAndRefFilesDontMatchWarningAddToRefListItemFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The script file &apos;{0}&apos; is outside the &apos;tools&apos; folder and hence will not be executed during installation of this package. Move it into the &apos;tools&apos; folder..
        /// </summary>
        public static string ScriptOutsideToolsWarning {
            get {
                return ResourceManager.GetString("ScriptOutsideToolsWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The license identifier &apos;{0}&apos; is not recognized by the current toolset..
        /// </summary>
        public static string UnrecognizedLicenseIdentifier {
            get {
                return ResourceManager.GetString("UnrecognizedLicenseIdentifier", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The script file &apos;{0}&apos; is not recognized by NuGet and hence will not be executed during installation of this package. Rename it to install.ps1, uninstall.ps1 or init.ps1 and place it directly under &apos;tools&apos;..
        /// </summary>
        public static string UnrecognizedScriptWarning {
            get {
                return ResourceManager.GetString("UnrecognizedScriptWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The version of dependency &apos;{0}&apos; is not specified. Specify the version of dependency and rebuild your package..
        /// </summary>
        public static string UnspecifiedDependencyVersionWarning {
            get {
                return ResourceManager.GetString("UnspecifiedDependencyVersionWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file at &apos;{0}&apos; uses the obsolete &apos;WinRT&apos; as the framework folder. Replace &apos;WinRT&apos; or &apos;WinRT45&apos; with &apos;NetCore45&apos;..
        /// </summary>
        public static string WinRTObsoleteWarning {
            get {
                return ResourceManager.GetString("WinRTObsoleteWarning", resourceCulture);
            }
        }
    }
}
