using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Newtonsoft.Json;
using System;

namespace f14.AspNetCore.FileManager.Handlers
{
    public abstract class BaseOperationHandler<TParam, TResult> : IOperationHandler<TParam, TResult>, IJOperationHandler<TParam, TResult>
        where TParam : BaseParam
        where TResult : BaseResult
    {
        private string _rootPath;
        private TResult _result;
        private Action<TParam> _preRunProcessor;

        public BaseOperationHandler(TResult result, string rootPath)
        {
            ExHelper.NotNull(() => result);

            _result = result;
            _rootPath = rootPath;
        }

        protected string RootPath => _rootPath;
        protected TResult Result => _result;

        public IJOperationHandler<TParam, TResult> OnParamParsed(Action<TParam> processor)
        {
            ExHelper.NotNull(() => processor);
            _preRunProcessor = processor;
            return this;
        }

        public abstract TResult Run(TParam param);

        public TResult Run(string jsonParam)
        {
            return Run(jsonParam, null);
        }

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

        protected virtual void OnErrorReport(string errorMsg)
        {
            Result.Errors?.Add(errorMsg);
        }
    }
}
