using Newtonsoft.Json;
using System;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Provides a base implementation of <see cref="IOperationHandler{TParam, TResult}"/> and <see cref="IJOperationHandler{TParam, TResult}"/>.
    /// </summary>
    /// <typeparam name="TParam">Type of request param.</typeparam>
    /// <typeparam name="TResult">Type of response result.</typeparam>
    public abstract class BaseOperationHandler<TParam, TResult> : IOperationHandler<TParam, TResult>, IJOperationHandler<TParam, TResult>
        where TParam : BaseParam
        where TResult : BaseResult
    {
        private string _rootPath;
        private TResult _result;
        private Action<TParam> _preRunProcessor;

        /// <summary>
        /// Base ctor.
        /// </summary>
        /// <param name="result">The response result instance.</param>
        /// <param name="rootPath">The path to root folder.</param>
        protected BaseOperationHandler(TResult result, string rootPath)
        {
            ExHelper.NotNull(() => result);

            _result = result;
            _rootPath = rootPath;
        }
        /// <summary>
        /// The path to root folder.
        /// </summary>
        protected string RootPath => _rootPath;
        /// <summary>
        /// The response result.
        /// </summary>
        protected TResult Result => _result;
        /// <summary>
        /// Sets an action, which will execute after the request param parsed.
        /// </summary>
        /// <param name="processor">The param processor action.</param>
        /// <returns>Current object.</returns>
        public IJOperationHandler<TParam, TResult> OnParamParsed(Action<TParam> processor)
        {
            ExHelper.NotNull(() => processor);
            _preRunProcessor = processor;
            return this;
        }
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param.</param>
        /// <returns>The operation result.</returns>
        public abstract TResult Run(TParam param);
        /// <summary>
        /// Performs the operation processing using json string param.
        /// </summary>
        /// <param name="jsonParam">The json string.</param>
        /// <returns>The operation result.</returns>
        public TResult Run(string jsonParam)
        {
            return Run(jsonParam, null);
        }
        /// <summary>
        /// Performs the operation processing using json string param.
        /// </summary>
        /// <param name="jsonParam">The json string.</param>
        /// <param name="settings">The serializer settings.</param>
        /// <returns>The operation result.</returns>
        public virtual TResult Run(string jsonParam, JsonSerializerSettings settings)
        {
            TParam param;
            if (settings == null)
            {
                param = JsonConvert.DeserializeObject<TParam>(jsonParam);
            }
            else
            {
                param = JsonConvert.DeserializeObject<TParam>(jsonParam, settings);
            }

            _preRunProcessor?.Invoke(param);

            return Run(param);
        }
        /// <summary>
        /// Adds an error message to the response instance.
        /// </summary>
        /// <param name="errorMsg">The error message.</param>
        protected virtual void OnErrorReport(string errorMsg)
        {
            Result.Errors?.Add(errorMsg);
        }
    }
}
