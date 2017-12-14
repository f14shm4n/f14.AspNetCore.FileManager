using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data.Results
{
    /// <summary>
    /// Supports affected field object.
    /// </summary>
    public class AffectedResult : BaseResult
    {
        /// <summary>
        /// Affected objects.
        /// </summary>
        public int Affected { get; set; }
    }
}
