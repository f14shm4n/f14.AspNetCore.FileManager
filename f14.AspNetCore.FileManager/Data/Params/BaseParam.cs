using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data.Params
{
    /// <summary>
    /// Represents the base object model for request data.
    /// </summary>
    public class BaseParam
    {
        /// <summary>
        /// Path to the work folder. All specified action will be performed in this folder.
        /// </summary>
        public string CurrentFolderPath { get; set; }
    }
}
