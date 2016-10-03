﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace NuGet.Credentials
{
    /// <summary>
    /// PluginUnexpectedStatusException results when a plugin credential provider returns an unexpected status,
    /// one not enumerated in PluginCredentialResponseExitCode.
    /// This typically occurs when a plugin throws a terminating exception.
    /// </summary>
    [Serializable]
    public class PluginUnexpectedStatusException : PluginException
    {
        public PluginUnexpectedStatusException() { }

        public PluginUnexpectedStatusException(string message) : base(message) { }

        public PluginUnexpectedStatusException(string message, Exception inner) : base(message, inner) { }

        protected PluginUnexpectedStatusException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }

        public static PluginException CreateUnexpectedStatusMessage(
            string path, PluginCredentialResponseExitCode status)
        {
            return new PluginUnexpectedStatusException(
                string.Format(Resources.PluginException_UnexpectedStatus_Format, path, status));
        }
    }
}
