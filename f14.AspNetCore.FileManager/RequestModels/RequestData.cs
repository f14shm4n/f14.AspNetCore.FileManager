using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// Base object model for request data.
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// Request type like create folder, delete file and etc.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Path to the specified file system object.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Determines whether the current request contains a type or not.
        /// </summary>
        [JsonIgnore]
        public bool HasType => !string.IsNullOrWhiteSpace(Type);
    }
}
