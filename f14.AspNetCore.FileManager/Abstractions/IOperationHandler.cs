using System;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Provides a basic interface for processing requests from the user.
    /// </summary>
    /// <typeparam name="TParam">Type of request param.</typeparam>
    /// <typeparam name="TResult">Type of response object.</typeparam>
    public interface IOperationHandler<TParam, TResult>
        where TParam : IParam
        where TResult : IResult
    {
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param.</param>
        /// <returns>The operation result.</returns>
        TResult Run(TParam param);
    }
}
