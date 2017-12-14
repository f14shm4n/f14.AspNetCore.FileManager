using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data
{
    /// <summary>
    /// Represents the base object model for the any action target.
    /// </summary>
    public class BaseActionTarget
    {
        /// <summary>
        /// The name of the object.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Determines whether the object is a file.
        /// </summary>
        public bool IsFile { get; set; }
    }
}
