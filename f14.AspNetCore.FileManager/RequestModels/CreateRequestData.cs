using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.RequestModels
{
    /// <summary>
    /// The create object model for the corresponding request.
    /// </summary>
    public class CreateRequestData : RequestData
    {
        /// <summary>
        /// Name of the object that must be created.
        /// </summary>
        public string Name { get; set; }
    }
}
