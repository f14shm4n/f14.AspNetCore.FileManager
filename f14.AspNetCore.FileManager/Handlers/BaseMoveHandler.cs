using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using f14.AspNetCore.FileManager.Data;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;

namespace f14.AspNetCore.FileManager.Handlers
{
    public abstract class BaseMoveHandler<TParam, TResult> : BaseOperationHandler<TParam, TResult>
        where TParam : MoveParam
        where TResult : MoveResult
    {
        public BaseMoveHandler(TResult result, string rootPath) : base(result, rootPath)
        {
        }

        public override TResult Run(TParam param)
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

            string srcDirPath = PathHelper.GetFullPath(RootPath, param.SourceDirectory);
            string dstDirPath = PathHelper.GetFullPath(RootPath, param.DestinationDirectory);

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
