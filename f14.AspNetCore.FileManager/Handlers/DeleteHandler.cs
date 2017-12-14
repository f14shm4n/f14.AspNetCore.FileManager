using f14.AspNetCore.FileManager.Data.Params;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;

namespace f14.AspNetCore.FileManager.Handlers
{
    public class DeleteHandler : BaseOperationHandler<DeleteParam>
    {
        public DeleteHandler(IHostingEnvironment env) : base(new DeleteResult(), env)
        {
        }

        public override BaseResult Run(DeleteParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                Action<AffectedResult> incAffectedAct = r => r.Affected++;

                foreach (var n in param.Targets)
                {
                    string targetObjPath = Path.Combine(fullPath, n);
                    try
                    {
                        if (File.Exists(targetObjPath))
                        {
                            File.Delete(targetObjPath);
                            DoWithResult(incAffectedAct);
                        }
                        else if (Directory.Exists(targetObjPath))
                        {
                            Directory.Delete(targetObjPath, true);
                            DoWithResult(incAffectedAct);
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
