using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Data.Params
{
    /// <summary>
    /// Represents the object model for the folder struct action request.
    /// </summary>
    public class FolderStructParam : BaseParam
    {        
        /// <summary>
        /// The file extensions collection.
        /// </summary>
        public string[] FileExtensions { get; set; } = new string[0];        
    }
}
