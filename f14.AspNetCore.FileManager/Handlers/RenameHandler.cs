using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface IRenameHandler : IOperationHandler<RenameParam, RenameResult>, IJOperationHandler<RenameParam, RenameResult>
    {

    }

    public sealed class RenameHandler : BaseOperationHandler<RenameParam, RenameResult>, IRenameHandler
    {
        public RenameHandler(IHostingEnvironment env) : base(new RenameResult(), env.WebRootPath)
        {
        }

        public override RenameResult Run(RenameParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(RootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                if (param.Targets.Count > 0)
                {
                    foreach (var rfi in param.Targets)
                    {
                        string srcPath = Path.Combine(fullPath, rfi.OldName);
                        string dstPath = Path.Combine(fullPath, rfi.Name);

                        try
                        {
                            if (rfi.IsFile)
                            {
                                FileIO.RenameFile(srcPath, dstPath);
                            }
                            else
                            {
                                FileIO.RenameFolder(srcPath, dstPath);
                            }

                            Result.Affected++;
                            Result.RenamedObjects.Add(rfi);
                        }
                        catch (Exception ex)
                        {
                            OnErrorReport($"Rename failed. Name info: {rfi.OldName} -> {rfi.Name}. Ex: {ex.Message}");
                        }
                    }
                }
                else
                {
                    OnErrorReport("The request data does not contains any rename targets.");
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
