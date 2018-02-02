using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Contains all default file manager operation keys.
    /// </summary>
    public sealed class DefaultOperations
    {
        /// <summary>
        /// Folder struct action key.
        /// </summary>
        public const string FolderStruct = "struct";
        /// <summary>
        /// Create folder action key.
        /// </summary>
        public const string CreateFolder = "create_folder";
        /// <summary>
        /// Rename action key.
        /// </summary>
        public const string Rename = "rename";
        /// <summary>
        /// Delete action key.
        /// </summary>        
        public const string Delete = "delete";
        /// <summary>
        /// Move
        /// </summary>
        public const string Move = "move";
        /// <summary>
        /// Copy action key.
        /// </summary>
        public const string Copy = "copy";        
    }
}
