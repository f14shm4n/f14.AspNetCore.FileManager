using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using f14.IO;
using System.Linq;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using f14.AspNetCore.FileManager.Data;

namespace f14.AspNetCore.FileManager.Handlers
{
    public class FolderStructHandler : BaseOperationHandler<FolderStructParam>
    {
        public FolderStructHandler(IHostingEnvironment env) : base(new FolderStructResult(), env)
        {
        }

        public override BaseResult Run(FolderStructParam param)
        {
            ExHelper.NotNull(() => param);

            if (param.FileExtensions == null)
            {
                param.FileExtensions = new string[0];
            }

            string fullPath = PathHelper.GetFullPath(HostEnv.WebRootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                try
                {
                    var dir = new DirectoryInfo(fullPath);
                    var files = (param.FileExtensions != null && param.FileExtensions.Length > 0 ? dir.GetFilesByExtensions(param.FileExtensions) : dir.EnumerateFiles())
                        .Select(x => new FileInfoProxy { Name = x.Name }).ToList();
                    var folders = dir.EnumerateDirectories().Select(x => new FileInfoProxy { Name = x.Name }).ToList();

                    DoWithResult<FolderStructResult>(r =>
                    {
                        r.Folders = folders;
                        r.Files = files;
                    });
                }
                catch (Exception ex)
                {
                    OnErrorReport($"Cannot get folder for path: {param.CurrentFolderPath}. " + ex.Message);
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
