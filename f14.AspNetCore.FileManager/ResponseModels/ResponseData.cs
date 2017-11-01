using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.ResponseModels
{
    /// <summary>
    /// Represents the base response data object for the specified request.
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Contains the errors that has occurred while handling specified request.
        /// </summary>
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}
