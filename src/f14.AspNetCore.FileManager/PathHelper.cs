using System;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Helper for the request data operations.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Checks specified string is valid for naming files or folders.
        /// </summary>
        /// <param name="name">Provided name for file or folder.</param>
        /// <returns>True - is valid; false - not valid.</returns>
        public static bool IsValidFolderOrFileName(string name) => name.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0;

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
