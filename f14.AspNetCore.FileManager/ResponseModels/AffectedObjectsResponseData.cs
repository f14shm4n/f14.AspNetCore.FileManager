using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.ResponseModels
{
    /// <summary>
    /// Supports affected field object.
    /// </summary>
    public class AffectedObjectsResponseData : ResponseData
    {
        /// <summary>
        /// Affected objects.
        /// </summary>
        public int Affected { get; set; }
    }
}
