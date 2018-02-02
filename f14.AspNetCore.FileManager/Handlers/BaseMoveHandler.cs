using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// Base implementation for move object action.
    /// </summary>
    /// <typeparam name="TParam">Type of request param.</typeparam>
    /// <typeparam name="TResult">Type of response result.</typeparam>
    public abstract class BaseMoveHandler<TParam, TResult> : BaseOperationHandler<TParam, TResult>
        where TParam : MoveParam
        where TResult : MoveResult
    {
        /// <summary>
        /// Base ctor.
        /// </summary>
        /// <param name="result">The response result instance.</param>
        /// <param name="rootPath">The path to root folder.</param>
        protected BaseMoveHandler(TResult result, string rootPath) : base(result, rootPath)
        {
        }
        /// <summary>
        /// Performs the operation processing.
        /// </summary>
        /// <param name="param">The specified param.</param>
        /// <returns>The operation result.</returns>
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
        /// <summary>
        /// Custom method for processing the single target object.
        /// </summary>
        /// <param name="t">The info about target.</param>
        /// <param name="srcObjPath">The source path to the object.</param>
        /// <param name="dstObjPath">The destination path to the object.</param>
        /// <param name="overwrite">Overwrite end point object or not.</param>
        protected abstract void OnProcess(BaseActionTarget t, string srcObjPath, string dstObjPath, bool overwrite);
    }
}
