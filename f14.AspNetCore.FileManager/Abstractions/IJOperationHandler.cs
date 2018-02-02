using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Provides a basic interface for processing requests from the user, this interface can parse <see cref="BaseParam"/> from json string.
    /// </summary>
    /// <typeparam name="TParam">Type of request param.</typeparam>
    /// <typeparam name="TResult">Type of response object.</typeparam>
    public interface IJOperationHandler<TParam, TResult> : IOperationHandler<TParam, TResult>
        where TParam : IParam
        where TResult : IResult
    {
        /// <summary>
        /// Sets the action to be performed after the json param analysis.
        /// </summary>
        /// <param name="processor">The specified action.</param>
        /// <returns>This object.</returns>
        IJOperationHandler<TParam, TResult> OnParamParsed(Action<TParam> processor);
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param as json string.</param>
        /// <returns>The operation result.</returns>
        TResult Run(string jsonParam);
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param as json string.</param>
        /// <param name="settings">The settings for the deserialization.</param>
        /// <returns>The operation result.</returns>
        TResult Run(string jsonParam, JsonSerializerSettings settings);
    }
}
