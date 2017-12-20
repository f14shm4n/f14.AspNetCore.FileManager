using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface IDeleteHandler : IOperationHandler<DeleteParam, DeleteResult>, IJOperationHandler<DeleteParam, DeleteResult>
    {

    }

    public class DeleteHandler : BaseOperationHandler<DeleteParam, DeleteResult>, IDeleteHandler
    {
        public DeleteHandler(IHostingEnvironment env) : base(new DeleteResult(), env.WebRootPath)
        {
        }

        public override DeleteResult Run(DeleteParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(RootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                foreach (var n in param.Targets)
                {
                    string targetObjPath = Path.Combine(fullPath, n);
                    try
                    {
                        if (File.Exists(targetObjPath))
                        {
                            File.Delete(targetObjPath);
                            Result.Affected++;
                        }
                        else if (Directory.Exists(targetObjPath))
                        {
                            Directory.Delete(targetObjPath, true);
                            Result.Affected++;
                        }
                    }
                    catch (Exception ex)
                    {
                        OnErrorReport($"Delete action is failed. Object name: {n} Ex: {ex.Message}");
                    }
                }
            }
            else
            {
                OnErrorReport("The work folder does not exists on the real file system. Check work folder path: " + param.CurrentFolderPath);
            }

            return Result;
        }
    }
}
