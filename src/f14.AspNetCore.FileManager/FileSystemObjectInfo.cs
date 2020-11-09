using System.Collections.Generic;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Represents the proxy object for the file system info.
    /// </summary>
    public class FileSystemObjectInfo
    {
        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="fileSystemInfo">Source object.</param>
        public FileSystemObjectInfo(FileSystemInfo fileSystemInfo)
        {
            Name = fileSystemInfo.Name;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="name">Object name.</param>
        /// <param name="properties">Object properties.</param>
        public FileSystemObjectInfo(string name, Dictionary<string, string> properties)
        {
            Name = name;
            Properties = properties;
        }

        /// <summary>
        /// The file or folder name.
        /// <para>Includes only name of object, not the path to the object.</para>
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Other object properties.
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
