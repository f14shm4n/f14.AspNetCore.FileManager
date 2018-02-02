using f14.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Represents the object model for the move action request.
    /// </summary>
    public class MoveParam : TargetCollectionParam<BaseActionTarget>
    {
        /// <summary>
        /// Source folder path.
        /// </summary>
        public string SourceDirectory { get; set; }
        /// <summary>
        /// Destination folder path.
        /// </summary>
        public string DestinationDirectory { get; set; }
        /// <summary>
        /// Determines whether overwrite files or not.
        /// </summary>
        public bool Overwrite { get; set; } = false;
    }
}
