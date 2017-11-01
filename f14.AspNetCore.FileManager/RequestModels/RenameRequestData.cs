using f14.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// The rename object model for the corresponding request.
    /// </summary>
    public class RenameRequestData : RequestData
    {
        /// <summary>
        /// Contains info for the renaming object.
        /// </summary>
        public class RenameFileInfo
        {
            /// <summary>
            /// Old object name.
            /// </summary>
            public string OldName { get; set; }
            /// <summary>
            /// New object name.
            /// </summary>
            public string NewName { get; set; }
            /// <summary>
            /// Determines whether the object is a file.
            /// </summary>
            public bool IsFile { get; set; }
        }

        /// <summary>
        /// Objects to rename.
        /// </summary>
        public List<RenameFileInfo> Renames { get; set; } = new List<RenameFileInfo>();
    }
}
