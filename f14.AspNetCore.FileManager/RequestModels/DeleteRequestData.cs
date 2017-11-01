using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// The delete object model for the corresponding request.
    /// </summary>
    public class DeleteRequestData : RequestData
    {
        /// <summary>
        /// Object names that must be deleted.
        /// </summary>
        public List<string> ObjectNames { get; set; } = new List<string>();        
    }
}
