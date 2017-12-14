using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;

namespace f14.AspNetCore.FileManager.Handlers
{
    public class CreateFolderHandler : BaseOperationHandler<CreateFolderParam>
    {
        public CreateFolderHandler(IHostingEnvironment env) : base(new CreateFolderResult(), env)
        {
        }

        public override BaseResult Run(CreateFolderParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                if (!param.Name.Contains('\\') && !param.Name.Contains('/'))
                {
                    string fullPathToNewFolder = Path.Combine(fullPath, param.Name);

                    try
                    {
                        Directory.CreateDirectory(fullPathToNewFolder);
                    }
                    catch (Exception ex)
                    {
                        OnErrorReport($"Unable to create folder. Ex: {ex.Message}");
                    }
                }
                else
                {
                    OnErrorReport("The new folder name contains invalid chars. New folder name cannot contains slashes.");
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
