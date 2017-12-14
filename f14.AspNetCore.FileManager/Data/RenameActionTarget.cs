using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data
{
    /// <summary>
    /// Represents the object model for the rename action target.
    /// </summary>
    public class RenameActionTarget : BaseActionTarget
    {
        /// <summary>
        /// The old name of the object.
        /// </summary>
        public string OldName { get; set; }
    }
}
