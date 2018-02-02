using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Interface for all response objects.
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// The collection of the errors.
        /// </summary>
        List<string> Errors { get; }
    }
}
