using f14.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// The move object model for the corresponding request.
    /// </summary>
    public class MoveRequestData : RequestData
    {
        /// <summary>
        /// Contains info about objects to move.
        /// </summary>
        public class MoveTarget
        {
            /// <summary>
            /// Object name.
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Determines whether the object is a file.
            /// </summary>
            public bool IsFile { get; set; }
        }

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
        /// <summary>
        /// Objects to move.
        /// </summary>
        public List<MoveTarget> Targets { get; set; } = new List<MoveTarget>();
    }
}
