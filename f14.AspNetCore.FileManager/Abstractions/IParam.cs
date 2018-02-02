using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Interface for all request param objects.
    /// </summary>
    public interface IParam
    {
        /// <summary>
        /// Returns the path for current folder. (Work folder).
        /// </summary>
        string CurrentFolderPath { get; }
    }
}
