using f14.AspNetCore.FileManager.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data.Results
{
    /// <summary>
    /// Represents the base response data object for the specified request.
    /// </summary>
    public class BaseResult : IResult
    {
        /// <summary>
        /// Contains the errors that has occurred while handling specified request.
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
    }
}
