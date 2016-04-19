﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Common
{
    /// <summary>
    /// File operation helpers.
    /// </summary>
    public static class FileUtility
    {
        private const int MaxTries = 3;

        /// <summary>
        /// Move a file with retries.
        /// </summary>
        public static void Move(string sourceFileName, string destFileName)
        {
            if (sourceFileName == null)
            {
                throw new ArgumentNullException(nameof(sourceFileName));
            }

            if (destFileName == null)
            {
                throw new ArgumentNullException(nameof(destFileName));
            }

            // Run at least and continue until the move succeeds or this times out
            for (int i=0; i < MaxTries; i++)
            {
                // Ignore exceptions for the first attempts
                try
                {
                    File.Move(sourceFileName, destFileName);

                    break;
                }
                catch (Exception ex) when ((i < (MaxTries - 1)) && (ex is UnauthorizedAccessException || ex is IOException))
                {
                    Sleep(100);
                }
            }
        }

        /// <summary>
        /// Delete a file with retries.
        /// </summary>
        public static void Delete(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            // Run at least and continue until the move succeeds or this times out
            for (int i = 0; i < MaxTries; i++)
            {
                // Ignore exceptions for the first attempts
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    break;
                }
                catch (Exception ex) when ((i < (MaxTries - 1)) && (ex is UnauthorizedAccessException || ex is IOException))
                {
                    Sleep(100);
                }
            }
        }

        private static void Sleep(int ms)
        {
            // Sleep sync
            Task.Delay(ms).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
