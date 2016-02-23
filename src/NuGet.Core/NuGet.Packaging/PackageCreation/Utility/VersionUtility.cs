﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using NuGet.Packaging.PackageCreation.Resources;
using CompatibilityMapping = System.Collections.Generic.Dictionary<string, string[]>;

namespace NuGet.Packaging
{
    public static class VersionUtility
    {
        private const string NetFrameworkIdentifier = ".NETFramework";
        private const string NetCoreFrameworkIdentifier = ".NETCore";
        private const string PortableFrameworkIdentifier = ".NETPortable";
        private const string NetPlatformFrameworkIdentifier = ".NETPlatform";
        private const string NetPlatformFrameworkShortName = "dotnet";
        private const string AspNetFrameworkIdentifier = "ASP.Net";
        private const string AspNetCoreFrameworkIdentifier = "ASP.NetCore";
        private const string DnxFrameworkIdentifier = "DNX";
        private const string DnxFrameworkShortName = "dnx";
        private const string DnxCoreFrameworkIdentifier = "DNXCore";
        private const string DnxCoreFrameworkShortName = "dnxcore";
        private const string UAPFrameworkIdentifier = "UAP";
        private const string UAPFrameworkShortName = "uap";
        private const string LessThanOrEqualTo = "\u2264";
        private const string GreaterThanOrEqualTo = "\u2265";

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Security",
            "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "The type FrameworkName is immutable.")]
        public static readonly FrameworkName EmptyFramework = new FrameworkName("NoFramework", new Version(0, 0));

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Security",
            "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "The type FrameworkName is immutable.")]
        public static readonly FrameworkName UnsupportedFrameworkName = new FrameworkName("Unsupported", new Version(0, 0));
        private static readonly Version _emptyVersion = new Version(0, 0);

        private static readonly Dictionary<string, string> _knownIdentifiers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            // FYI, the keys are CASE-INSENSITIVE

            // .NET Desktop
            { "NET", NetFrameworkIdentifier },
            { ".NET", NetFrameworkIdentifier },
            { "NETFramework", NetFrameworkIdentifier },
            { ".NETFramework", NetFrameworkIdentifier },

            // .NET Core
            { "NETCore", NetCoreFrameworkIdentifier},
            { ".NETCore", NetCoreFrameworkIdentifier},
            { "WinRT", NetCoreFrameworkIdentifier},     // 'WinRT' is now deprecated. Use 'Windows' or 'win' instead.

            // .NET Micro Framework
            { ".NETMicroFramework", ".NETMicroFramework" },
            { "netmf", ".NETMicroFramework" },

            // Silverlight
            { "SL", "Silverlight" },
            { "Silverlight", "Silverlight" },

            // Portable Class Libraries
            { ".NETPortable", PortableFrameworkIdentifier },
            { "NETPortable", PortableFrameworkIdentifier },
            { "portable", PortableFrameworkIdentifier },

            // Windows Phone
            { "wp", "WindowsPhone" },
            { "WindowsPhone", "WindowsPhone" },
            { "WindowsPhoneApp", "WindowsPhoneApp"},
            { "wpa", "WindowsPhoneApp"},
            
            // Windows
            { "Windows", "Windows" },
            { "win", "Windows" },

            // ASP.Net (TODO: Remove these eventually)
            { "aspnet", AspNetFrameworkIdentifier },
            { "aspnetcore", AspNetCoreFrameworkIdentifier },
            { "asp.net", AspNetFrameworkIdentifier },
            { "asp.netcore", AspNetCoreFrameworkIdentifier },

            // DNX 
            { DnxFrameworkShortName, DnxFrameworkIdentifier },
            { DnxCoreFrameworkShortName, DnxCoreFrameworkIdentifier },

            // Dotnet
            { NetPlatformFrameworkShortName, NetPlatformFrameworkIdentifier },
            { NetPlatformFrameworkIdentifier, NetPlatformFrameworkIdentifier },

            // UAP
            { UAPFrameworkShortName, UAPFrameworkIdentifier },

            // Native
            { "native", "native"},
            
            // Mono/Xamarin
            { "MonoAndroid", "MonoAndroid" },
            { "MonoTouch", "MonoTouch" },
            { "MonoMac", "MonoMac" },
            { "Xamarin.iOS", "Xamarin.iOS" },
            { "XamariniOS", "Xamarin.iOS" },
            { "Xamarin.Mac", "Xamarin.Mac" },
            { "XamarinMac", "Xamarin.Mac" },
            { "Xamarin.PlayStationThree", "Xamarin.PlayStation3" },
            { "XamarinPlayStationThree", "Xamarin.PlayStation3" },
            { "XamarinPSThree", "Xamarin.PlayStation3" },
            { "Xamarin.PlayStationFour", "Xamarin.PlayStation4" },
            { "XamarinPlayStationFour", "Xamarin.PlayStation4" },
            { "XamarinPSFour", "Xamarin.PlayStation4" },
            { "Xamarin.PlayStationVita", "Xamarin.PlayStationVita" },
            { "XamarinPlayStationVita", "Xamarin.PlayStationVita" },
            { "XamarinPSVita", "Xamarin.PlayStationVita" },
            { "Xamarin.TVOS", "Xamarin.TVOS" },
            { "XamarinTVOS", "Xamarin.TVOS" },
            { "Xamarin.WatchOS", "Xamarin.WatchOS" },
            { "XamarinWatchOS", "Xamarin.WatchOS" },
            { "Xamarin.XboxThreeSixty", "Xamarin.Xbox360" },
            { "XamarinXboxThreeSixty", "Xamarin.Xbox360" },
            { "Xamarin.XboxOne", "Xamarin.XboxOne" },
            { "XamarinXboxOne", "Xamarin.XboxOne" }
        };

        private static readonly Dictionary<string, string> _knownProfiles = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            { "Client", "Client" },
            { "WP", "WindowsPhone" },
            { "WP71", "WindowsPhone71" },
            { "CF", "CompactFramework" },
            { "Full", String.Empty }
        };

        private static readonly Dictionary<string, string> _identifierToFrameworkFolder = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            { NetFrameworkIdentifier, "net" },
            { ".NETMicroFramework", "netmf" },
            { DnxFrameworkIdentifier, DnxFrameworkShortName },
            { DnxCoreFrameworkIdentifier, DnxCoreFrameworkShortName },
            { NetPlatformFrameworkIdentifier, NetPlatformFrameworkShortName },
            { AspNetFrameworkIdentifier, "aspnet" },
            { AspNetCoreFrameworkIdentifier, "aspnetcore" },
            { "Silverlight", "sl" },
            { ".NETCore45", "win"},
            { ".NETCore451", "win81"},
            { "Windows", "win"},
            { ".NETPortable", "portable" },
            { "WindowsPhone", "wp"},
            { "WindowsPhoneApp", "wpa"},
            { "Xamarin.iOS", "xamarinios" },
            { "Xamarin.Mac", "xamarinmac" },
            { "Xamarin.PlayStation3", "xamarinpsthree" },
            { "Xamarin.PlayStation4", "xamarinpsfour" },
            { "Xamarin.PlayStationVita", "xamarinpsvita" },
            { "Xamarin.TVOS", "xamarintvos" },
            { "Xamarin.WatchOS", "xamarinwatchos" },
            { "Xamarin.Xbox360", "xamarinxboxthreesixty" },
            { "Xamarin.XboxOne", "xamarinxboxone" },
        };

        private static readonly Dictionary<string, string> _identifierToProfileFolder = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
            { "WindowsPhone", "wp" },
            { "WindowsPhone71", "wp71" },
            { "CompactFramework", "cf" }
        };

        private static readonly Dictionary<string, CompatibilityMapping> _compatibiltyMapping = new Dictionary<string, CompatibilityMapping>(StringComparer.OrdinalIgnoreCase) {
            {
                // Client profile is compatible with the full framework (empty string is full)
                NetFrameworkIdentifier, new CompatibilityMapping(StringComparer.OrdinalIgnoreCase) {
                    { "", new [] { "Client" } },
                    { "Client", new [] { "" } }
                }
            },
            {
                "Silverlight", new CompatibilityMapping(StringComparer.OrdinalIgnoreCase) {
                    { "WindowsPhone", new[] { "WindowsPhone71" } },
                    { "WindowsPhone71", new[] { "WindowsPhone" } }
                }
            }
        };

        // These aliases allow us to accept 'wp', 'wp70', 'wp71', 'windows', 'windows8' as valid target farmework folders.
        private static readonly Dictionary<FrameworkName, FrameworkName> _frameworkNameAlias = new Dictionary<FrameworkName, FrameworkName>(FrameworkNameEqualityComparer.Default)
        {
            { new FrameworkName("WindowsPhone, Version=v0.0"), new FrameworkName("Silverlight, Version=v3.0, Profile=WindowsPhone") },
            { new FrameworkName("WindowsPhone, Version=v7.0"), new FrameworkName("Silverlight, Version=v3.0, Profile=WindowsPhone") },
            { new FrameworkName("WindowsPhone, Version=v7.1"), new FrameworkName("Silverlight, Version=v4.0, Profile=WindowsPhone71") },
            { new FrameworkName("WindowsPhone, Version=v8.0"), new FrameworkName("Silverlight, Version=v8.0, Profile=WindowsPhone") },
            { new FrameworkName("WindowsPhone, Version=v8.1"), new FrameworkName("Silverlight, Version=v8.1, Profile=WindowsPhone") },

            { new FrameworkName("Windows, Version=v0.0"), new FrameworkName(".NETCore, Version=v4.5") },
            { new FrameworkName("Windows, Version=v8.0"), new FrameworkName(".NETCore, Version=v4.5") },
            { new FrameworkName("Windows, Version=v8.1"), new FrameworkName(".NETCore, Version=v4.5.1") }
        };

        // See IsCompatible
        // The ASP.Net framework authors desire complete compatibility between 'aspnet50' and all 'net' versions
        // So we use this MaxVersion value to achieve complete compatiblilty.
        private static readonly Version MaxVersion = new Version(Int32.MaxValue, Int32.MaxValue, Int32.MaxValue, Int32.MaxValue);
        private static readonly Dictionary<string, FrameworkName> _equivalentProjectFrameworks = new Dictionary<string, FrameworkName>()
        {
            // Allow a core package to be installed in a dnxcore project 
            // { DnxCoreFrameworkIdentifier, new FrameworkName(CoreFrameworkIdentifier, MaxVersion) },

            // Allow an aspnetcore package to be installed in a dnxcore project 
            { DnxCoreFrameworkIdentifier, new FrameworkName(AspNetCoreFrameworkIdentifier, MaxVersion) },

            // Allow an aspnet package to be installed in a dnx project
            { DnxFrameworkIdentifier, new FrameworkName(AspNetFrameworkIdentifier, MaxVersion) },

            // Allow a net package to be installed in an aspnet (or dnx, transitively by above) project
            { AspNetFrameworkIdentifier, new FrameworkName(NetFrameworkIdentifier, MaxVersion) },
        };

        /// <summary>
        /// This function tries to normalize a string that represents framework version names into
        /// something a framework name that the package manager understands.
        /// </summary>
        public static FrameworkName ParseFrameworkName(string frameworkName)
        {
            if (frameworkName == null)
            {
                throw new ArgumentNullException(nameof(frameworkName));
            }

            // {Identifier}{Version}-{Profile}

            // Split the framework name into 3 parts, identifier, version and profile.
            string identifierPart = null;
            string versionPart = null;

            string[] parts = frameworkName.Split('-');

            if (parts.Length > 2)
            {
                throw new ArgumentException(NuGetResources.InvalidFrameworkNameFormat, "frameworkName");
            }

            string frameworkNameAndVersion = parts.Length > 0 ? parts[0].Trim() : null;
            string profilePart = parts.Length > 1 ? parts[1].Trim() : null;

            if (String.IsNullOrEmpty(frameworkNameAndVersion))
            {
                throw new ArgumentException(NuGetResources.MissingFrameworkName, "frameworkName");
            }

            // If we find a version then we try to split the framework name into 2 parts
            var versionMatch = Regex.Match(frameworkNameAndVersion, @"\d+");

            if (versionMatch.Success)
            {
                identifierPart = frameworkNameAndVersion.Substring(0, versionMatch.Index).Trim();
                versionPart = frameworkNameAndVersion.Substring(versionMatch.Index).Trim();
            }
            else
            {
                // Otherwise we take the whole name as an identifier
                identifierPart = frameworkNameAndVersion.Trim();
            }

            if (!String.IsNullOrEmpty(identifierPart))
            {
                // Try to normalize the identifier to a known identifier
                if (!_knownIdentifiers.TryGetValue(identifierPart, out identifierPart))
                {
                    return UnsupportedFrameworkName;
                }
            }

            if (!String.IsNullOrEmpty(profilePart))
            {
                string knownProfile;
                if (_knownProfiles.TryGetValue(profilePart, out knownProfile))
                {
                    profilePart = knownProfile;
                }
            }

            Version version = null;
            // We support version formats that are integers (40 becomes 4.0)
            int versionNumber;
            if (Int32.TryParse(versionPart, out versionNumber))
            {
                // Remove the extra numbers
                if (versionPart.Length > 4)
                {
                    versionPart = versionPart.Substring(0, 4);
                }

                // Make sure it has at least 2 digits so it parses as a valid version
                versionPart = versionPart.PadRight(2, '0');
                versionPart = String.Join(".", versionPart.ToCharArray());
            }

            // If we can't parse the version then use the default
            if (!Version.TryParse(versionPart, out version))
            {
                // We failed to parse the version string once more. So we need to decide if this is unsupported or if we use the default version.
                // This framework is unsupported if:
                // 1. The identifier part of the framework name is null.
                // 2. The version part is not null.
                if (String.IsNullOrEmpty(identifierPart) || !String.IsNullOrEmpty(versionPart))
                {
                    return UnsupportedFrameworkName;
                }

                // Use 5.0 instead of 0.0 as the default for NetPlatform
                version = identifierPart.Equals(NetPlatformFrameworkIdentifier) ? new Version(5, 0) : _emptyVersion;
            }

            if (String.IsNullOrEmpty(identifierPart))
            {
                identifierPart = NetFrameworkIdentifier;
            }

            // if this is a .NET Portable framework name, validate the profile part to ensure it is valid
            if (identifierPart.Equals(PortableFrameworkIdentifier, StringComparison.OrdinalIgnoreCase))
            {
                ValidatePortableFrameworkProfilePart(profilePart);
            }

            return new FrameworkName(identifierPart, version, profilePart);
        }

        internal static void ValidatePortableFrameworkProfilePart(string profilePart)
        {
            if (String.IsNullOrEmpty(profilePart))
            {
                throw new ArgumentException(NuGetResources.PortableFrameworkProfileEmpty, "profilePart");
            }

            if (profilePart.Contains('-'))
            {
                throw new ArgumentException(NuGetResources.PortableFrameworkProfileHasDash, "profilePart");
            }

            if (profilePart.Contains(' '))
            {
                throw new ArgumentException(NuGetResources.PortableFrameworkProfileHasSpace, "profilePart");
            }

            string[] parts = profilePart.Split('+');
            if (parts.Any(p => String.IsNullOrEmpty(p)))
            {
                throw new ArgumentException(NuGetResources.PortableFrameworkProfileComponentIsEmpty, "profilePart");
            }

            // Prevent portable framework inside a portable framework - Inception
            if (parts.Any(p => p.StartsWith("portable", StringComparison.OrdinalIgnoreCase)) ||
                parts.Any(p => p.StartsWith("NETPortable", StringComparison.OrdinalIgnoreCase)) ||
                parts.Any(p => p.StartsWith(".NETPortable", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException(NuGetResources.PortableFrameworkProfileComponentIsPortable, "profilePart");
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static FrameworkName ParseFrameworkNameFromFilePath(string filePath, out string effectivePath)
        {
            var knownFolders = new string[] 
            { 
                Constants.ContentDirectory,
                Constants.LibDirectory,
                Constants.ToolsDirectory,
                Constants.BuildDirectory
            };

            for (int i = 0; i < knownFolders.Length; i++)
            {
                string folderPrefix = knownFolders[i] + System.IO.Path.DirectorySeparatorChar;
                if (filePath.Length > folderPrefix.Length &&
                    filePath.StartsWith(folderPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    string frameworkPart = filePath.Substring(folderPrefix.Length);

                    try
                    {
                        return VersionUtility.ParseFrameworkFolderName(
                            frameworkPart,
                            strictParsing: knownFolders[i] == Constants.LibDirectory,
                            effectivePath: out effectivePath);
                    }
                    catch (ArgumentException)
                    {
                        // if the parsing fails, we treat it as if this file
                        // doesn't have target framework.
                        effectivePath = frameworkPart;
                        return null;
                    }
                }

            }

            effectivePath = filePath;
            return null;
        }

        /// <summary>
        /// Parses the specified string into FrameworkName object.
        /// </summary>
        /// <param name="path">The string to be parse.</param>
        /// <param name="strictParsing">if set to <c>true</c>, parse the first folder of path even if it is unrecognized framework.</param>
        /// <param name="effectivePath">returns the path after the parsed target framework</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#")]
        public static FrameworkName ParseFrameworkFolderName(string path, bool strictParsing, out string effectivePath)
        {
            // The path for a reference might look like this for assembly foo.dll:            
            // foo.dll
            // sub\foo.dll
            // {FrameworkName}{Version}\foo.dll
            // {FrameworkName}{Version}\sub1\foo.dll
            // {FrameworkName}{Version}\sub1\sub2\foo.dll

            // Get the target framework string if specified
            string targetFrameworkString = Path.GetDirectoryName(path).Split(Path.DirectorySeparatorChar).First();

            effectivePath = path;

            if (String.IsNullOrEmpty(targetFrameworkString))
            {
                return null;
            }

            var targetFramework = ParseFrameworkName(targetFrameworkString);
            if (strictParsing || targetFramework != UnsupportedFrameworkName)
            {
                // skip past the framework folder and the character \
                effectivePath = path.Substring(targetFrameworkString.Length + 1);
                return targetFramework;
            }

            return null;
        }
    }
}
