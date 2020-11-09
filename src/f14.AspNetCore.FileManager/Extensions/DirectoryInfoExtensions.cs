using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager.Extensions
{
    /// <summary>
    /// Provides extensions methods for <see cref="DirectoryInfo"/>.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Gets an enumerable of files filtered by specific extensions.
        /// </summary>
        /// <param name="dir">Target directory.</param>
        /// <param name="extensions">Desired extensions.</param>
        /// <returns>Enumerable.</returns>
        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, IEnumerable<string> extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentNullException("extensions");
            }
            if (!extensions.Any())
            {
                throw new InvalidOperationException("At least on extensions must be provided.");
            }
            if (dir == null)
            {
                throw new ArgumentNullException("dir");
            }
            return from x in dir.EnumerateFiles() where extensions.Contains(x.Extension) select x;
        }
    }
}
