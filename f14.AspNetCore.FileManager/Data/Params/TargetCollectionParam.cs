using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Represents the base object model for the action with generic collections of the targets.
    /// </summary>
    /// <typeparam name="T">Type of the targets.</typeparam>
    public class TargetCollectionParam<T> : BaseParam
    {
        /// <summary>
        /// Represents the specified target collection.
        /// </summary>
        public List<T> Targets { get; set; } = new List<T>();
    }
}
