using f14.AspNetCore.FileManager.Data.Params;
using System;
using System.Collections.Generic;
using System.Text;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace f14.AspNetCore.FileManager.Handlers
{
    public abstract class BaseOperationHandler<T> : IOperationHandler<T>, IJOperationHandler<T> where T : BaseParam
    {
        private IHostingEnvironment _env;
        private BaseResult _result;

        public BaseOperationHandler(BaseResult result, IHostingEnvironment env)
        {
            _result = result;
            _env = env;
        }

        protected IHostingEnvironment HostEnv => _env;
        protected BaseResult Result => _result;

        public abstract BaseResult Run(T param);

        public BaseResult Run(string jsonParam)
        {
            return Run(jsonParam, null);
        }

        public virtual BaseResult Run(string jsonParam, JsonSerializerSettings settings)
        {
            T param;
            if (settings == null)
            {
                param = JsonConvert.DeserializeObject<T>(jsonParam);
            }
            else
            {
                param = JsonConvert.DeserializeObject<T>(jsonParam, settings);
            }
            return Run(param);
        }

        protected virtual void OnErrorReport(string errorMsg)
        {
            _result.Errors.Add(errorMsg);
        }

        protected virtual void DoWithResult<TResult>(Action<TResult> action) where TResult : BaseResult
        {
            ExHelper.NotNull(() => action);
            if (_result is TResult)
            {
                action((TResult)_result);
            }
        }
    }
}
