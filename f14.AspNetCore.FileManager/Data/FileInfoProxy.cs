using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Represents the proxy object for the file system info.
    /// </summary>
    public class FileInfoProxy
    {
        /// <summary>
        /// The file or folder name. Includes only name of object, not the path to the object.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Other object properties.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }
    }
}
