using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Data.Params
{
    /// <summary>
    /// Represents the object model for the create folder action request.
    /// </summary>
    public class CreateFolderParam : BaseParam
    {
        /// <summary>
        /// Name of the object that must be created.
        /// </summary>
        public string Name { get; set; }
    }
}
