using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.ResponseModels
{
    /// <summary>
    /// Represents the response data object for the file system struct request.
    /// </summary>
    public class StructResponseData : ResponseData
    {
        /// <summary>
        /// Folders count.
        /// </summary>
        public int FolderCount { get; set; }
        /// <summary>
        /// Files count.
        /// </summary>        
        public int FileCount { get; set; }
        /// <summary>
        /// Folders collection.
        /// </summary>
        public IEnumerable<FileInfoProxy> Folders { get; set; }
        /// <summary>
        /// Files collection.
        /// </summary>
        public IEnumerable<FileInfoProxy> Files { get; set; }
    }
}
