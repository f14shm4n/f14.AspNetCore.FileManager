using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Helper for the request data operations.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Returns the path start with rootPath.
        /// </summary>
        /// <param name="rootPath">Path to app root.</param>
        /// <param name="currentPath">Relative path to the file or folder.</param>
        /// <returns>File or folder path.</returns>
        public static string GetFullPath(string rootPath, string currentPath)
        {
            string fullPath;
            if (!currentPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
            {
                string adjusted = currentPath.TrimStart('/', '\\');
                fullPath = System.IO.Path.Combine(rootPath, adjusted);
            }
            else
            {
                fullPath = currentPath;
            }
            return fullPath.TrimEnd('/', '\\');
        }
    }
}
