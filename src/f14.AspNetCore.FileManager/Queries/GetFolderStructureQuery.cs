using f14.AspNetCore.FileManager.Abstractions;
using System.Collections.Generic;

namespace f14.AspNetCore.FileManager.Queries
{
    /// <summary>
    /// Represents a request model for getting a folder structure.
    /// </summary>
    public sealed class GetFolderStructureQuery : FileManagerQuery<FolderStructureInfo>
    {
        /// <summary>
        /// The file extensions collection.
        /// </summary>
        public List<string>? FileExtensions { get; set; }
    }
}

