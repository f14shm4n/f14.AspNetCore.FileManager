using System;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Provides helper methods for exceptions.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Throws a <see cref="InvalidOperationException"/> if the directory does not exists.
        /// </summary>
        /// <param name="pathToDir">Path to the directory.</param>
        public static void ThrowIfDirectoryNotExists(string pathToDir)
        {
            if (!Directory.Exists(pathToDir))
            {
                throw new InvalidOperationException("The directory not exists.");
            }
        }

        /// <summary>
        /// Throws a <see cref="InvalidOperationException"/> if the provided name contains an invalid chars.
        /// </summary>
        /// <param name="name">Provided name for file or folder.</param>
        public static void ThrowIfFileOrFolderNameIsInvalid(string name)
        {
            if (!PathHelper.IsValidFolderOrFileName(name))
            {
                throw new InvalidOperationException($"The folder name contains invalid characters. Name: '{name}'");
            }
        }
    }
}
