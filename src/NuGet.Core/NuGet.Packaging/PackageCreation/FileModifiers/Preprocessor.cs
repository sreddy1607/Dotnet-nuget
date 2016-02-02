using NuGet.Packaging.PackageCreation.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace NuGet.Packaging
{
    /// <summary>
    /// Simple token replacement system for content files.
    /// </summary>
    public class Preprocessor
    {
        internal static string Process(IPackageFile file, IPropertyProvider propertyProvider)
        {
            using (var stream = file.GetStream())
            {
                return Process(stream, propertyProvider, throwIfNotFound: false);
            }
        }

        public static string Process(Stream stream, IPropertyProvider propertyProvider, bool throwIfNotFound = true)
        {
            string text = stream.ReadToEnd();
            var tokenizer = new Tokenizer(text);
            StringBuilder result = new StringBuilder();
            for (; ; )
            {
                Token token = tokenizer.Read();
                if (token == null)
                {
                    break;
                }

                if (token.Category == TokenCategory.Variable)
                {
                    var replaced = ReplaceToken(token.Value, propertyProvider, throwIfNotFound);
                    result.Append(replaced);
                }
                else
                {
                    result.Append(token.Value);
                }
            }

            return result.ToString();
        }

        private static string ReplaceToken(string propertyName, IPropertyProvider propertyProvider, bool throwIfNotFound)
        {
            var value = propertyProvider.GetPropertyValue(propertyName);
            if (value == null && throwIfNotFound)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, NuGetResources.TokenHasNoValue, propertyName));
            }
            return value;
        }
    }    
}
