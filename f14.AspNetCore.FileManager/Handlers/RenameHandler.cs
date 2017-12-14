using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using f14.IO;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;

namespace f14.AspNetCore.FileManager.Handlers
{
    public class RenameHandler : BaseOperationHandler<RenameParam>
    {
        public RenameHandler(IHostingEnvironment env) : base(new RenameResult(), env)
        {
        }

        public override BaseResult Run(RenameParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.CurrentFolderPath);

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
                            DoWithResult<AffectedResult>(r => r.Affected++);
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
