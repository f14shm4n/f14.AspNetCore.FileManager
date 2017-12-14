using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager.Handlers
{
    /// <summary>
    /// Provides a basic interface for processing requests from the user, this interface can parse <see cref="BaseParam"/> from json string.
    /// </summary>
    /// <typeparam name="T">Type of request param.</typeparam>
    public interface IJOperationHandler<T> : IOperationHandler<T> where T : BaseParam
    {
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param as json string.</param>
        /// <returns>The operation result.</returns>
        BaseResult Run(string jsonParam);
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param as json string.</param>
        /// <param name="settings">The settings for the deserialization.</param>
        /// <returns>The operation result.</returns>
        BaseResult Run(string jsonParam, JsonSerializerSettings settings);
    }
}
