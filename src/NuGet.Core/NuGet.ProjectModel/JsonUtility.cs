// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Packaging.Core;
using NuGet.Versioning;

namespace NuGet.ProjectModel
{
    internal static class JsonUtility
    {
        private static ReadOnlySpan<byte> Utf8Bom => new byte[] { 0xEF, 0xBB, 0xBF };

        internal static readonly char[] PathSplitChars = new[] { LockFile.DirectorySeparatorChar };

        /// <summary>
        /// JsonLoadSettings with line info and comments ignored.
        /// </summary>
        internal static readonly JsonLoadSettings DefaultLoadSettings = new JsonLoadSettings()
        {
            LineInfoHandling = LineInfoHandling.Ignore,
            CommentHandling = CommentHandling.Ignore
        };

        private static JsonSerializerOptions CreateJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
            };

            options.Converters.Add(new LockFileConverter());
            options.Converters.Add(new AssetsLogMessageConverter());
            options.Converters.Add(new LockFileLibraryConverter());
            options.Converters.Add(new LockFileTargetConverter());
            options.Converters.Add(new LockFileItemConverter<LockFileItem>());
            options.Converters.Add(new LockFileItemConverter<LockFileContentFile>());
            options.Converters.Add(new LockFileItemConverter<LockFileRuntimeTarget>());
            options.Converters.Add(new LockFileTargetLibraryConverter());
            options.Converters.Add(new ProjectFileDependencyGroupConverter());
            options.Converters.Add(new PackageSpecConverter());

            return options;
        }

        /// <summary>
        /// Load json from a file to a JObject using the default load settings.
        /// </summary>
        internal static JObject LoadJson(TextReader reader)
        {
            using (var jsonReader = new JsonTextReader(reader))
            {
                while (jsonReader.TokenType != JsonToken.StartObject)
                {
                    if (!jsonReader.Read())
                    {
                        throw new InvalidDataException();
                    }
                }

                return JObject.Load(jsonReader, DefaultLoadSettings);
            }
        }

        internal static async Task<T> LoadJsonAsync<T>(Stream stream)
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(stream, CreateJsonSerializerOptions());
        }

        internal static T LoadJson<T>(Stream stream)
        {
            var streamingJsonReader = new StreamingUtf8JsonReader(stream);
            var options = CreateJsonSerializerOptions();
            var objectConverter = (StreamableJsonConverter<T>)options.GetConverter(typeof(T));
            return objectConverter.ReadWithStream(ref streamingJsonReader, options);
        }

        internal static PackageDependency ReadPackageDependency(string property, JToken json)
        {
            var versionStr = json.Value<string>();
            return new PackageDependency(
                property,
                versionStr == null ? null : VersionRange.Parse(versionStr));
        }

        internal static JProperty WritePackageDependencyWithLegacyString(PackageDependency item)
        {
            return new JProperty(
                item.Id,
                WriteString(item.VersionRange?.ToNonSnapshotRange().ToLegacyShortString()));
        }

        internal static void WritePackageDependencyWithLegacyString(JsonWriter writer, PackageDependency item)
        {
            writer.WritePropertyName(item.Id);
            writer.WriteValue(item.VersionRange?.ToNonSnapshotRange().ToLegacyShortString());
        }

        internal static JProperty WritePackageDependency(PackageDependency item)
        {
            return new JProperty(
                item.Id,
                WriteString(item.VersionRange?.ToString()));
        }

        internal static void WritePackageDependency(JsonWriter writer, PackageDependency item)
        {
            writer.WritePropertyName(item.Id);
            writer.WriteValue(item.VersionRange?.ToString());
        }

        internal static TItem ReadProperty<TItem>(JObject jObject, string propertyName)
        {
            if (jObject != null)
            {
                JToken value;
                if (jObject.TryGetValue(propertyName, out value) && value != null)
                {
                    return value.Value<TItem>();
                }
            }

            return default(TItem);
        }

        internal static IList<TItem> ReadObject<TItem>(JObject jObject, Func<string, JToken, TItem> readItem)
        {
            if (jObject == null)
            {
                return new List<TItem>(0);
            }
            var items = new List<TItem>(jObject.Count);
            foreach (var child in jObject)
            {
                items.Add(readItem(child.Key, child.Value));
            }
            return items;
        }

        internal static JObject WriteObject<TItem>(IEnumerable<TItem> items, Func<TItem, JProperty> writeItem)
        {
            var array = new JObject();
            foreach (var item in items)
            {
                array.Add(writeItem(item));
            }
            return array;
        }

        internal static void WriteObject<TItem>(JsonWriter writer, IEnumerable<TItem> items, Action<JsonWriter, TItem> writeItem)
        {
            writer.WriteStartObject();

            foreach (var item in items)
            {
                writeItem(writer, item);
            }

            writer.WriteEndObject();
        }

        internal static int ReadInt(JToken cursor, string property, int defaultValue)
        {
            var valueToken = cursor[property];
            if (valueToken == null)
            {
                return defaultValue;
            }
            return valueToken.Value<int>();
        }

        internal static JToken WriteString(string item)
        {
            return item != null ? new JValue(item) : JValue.CreateNull();
        }
    }
}
