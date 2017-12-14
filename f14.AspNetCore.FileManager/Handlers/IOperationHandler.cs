using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;

namespace f14.AspNetCore.FileManager.Handlers
{
    /// <summary>
    /// Provides a basic interface for processing requests from the user.
    /// </summary>
    /// <typeparam name="T">Type of request param.</typeparam>
    public interface IOperationHandler<T> where T : BaseParam
    {
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param.</param>
        /// <returns>The operation result.</returns>
        BaseResult Run(T param);
    }
}
