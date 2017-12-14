using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using f14.IO;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using f14.AspNetCore.FileManager.Data;

namespace f14.AspNetCore.FileManager.Handlers
{
    public abstract class BaseMoveHandler<T> : BaseOperationHandler<T> where T : MoveParam
    {
        public BaseMoveHandler(BaseResult result, IHostingEnvironment env) : base(result, env)
        {
        }

        public override BaseResult Run(T param)
        {
            if (string.IsNullOrWhiteSpace(param.SourceDirectory) || string.IsNullOrWhiteSpace(param.DestinationDirectory))
            {
                OnErrorReport("Destination or Source directory path must be set.");
                return Result;
            }

            if (param.Targets.Count == 0)
            {
                OnErrorReport("Targets collection must be set.");
                return Result;
            }

            string srcDirPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.SourceDirectory);
            string dstDirPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.DestinationDirectory);

            if (Directory.Exists(srcDirPath) && Directory.Exists(dstDirPath))
            {
                foreach (var t in param.Targets)
                {
                    string srcObjPath = PathHelper.GetFullPath(srcDirPath, t.Name);
                    string dstObjPath = PathHelper.GetFullPath(dstDirPath, t.Name);

                    try
                    {
                        OnProcess(t, srcObjPath, dstObjPath, param.Overwrite);
                    }
                    catch (Exception ex)
                    {
                        OnErrorReport($"Action failed. Name info: {t.Name} Ex: {ex.Message}");
                    }
                }

                return Result;
            }
            else
            {
                OnErrorReport("Source or Destination folder does not exists.");
                return Result;
            }
        }

        protected abstract void OnProcess(BaseActionTarget t, string srcObjPath, string dstObjPath, bool overwrite);
    }
}
