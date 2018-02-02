using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Represents the response data object for the rename request.
    /// </summary>
    public class RenameResult : AffectedResult
    {
        /// <summary>
        /// The objects that have been renamed.
        /// </summary>
        public List<RenameActionTarget> RenamedObjects { get; set; } = new List<RenameActionTarget>();
    }
}
