using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Contains any service variables.
    /// </summary>
    public class ServiceVars
    {
        /// <summary>
        /// App root path.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// Create an instance of <see cref="ServiceVars"/>.
        /// </summary>
        /// <param name="rootPath">App root path.</param>
        public ServiceVars(string rootPath)
        {
            this.RootPath = rootPath;
        }
    }
}
