using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// The file system struct object model for the corresponding request.
    /// </summary>
    public class StructRequestData : RequestData
    {        
        /// <summary>
        /// The file extensions collection.
        /// </summary>
        public string[] FileExtensions { get; set; } = new string[0];        
    }
}
