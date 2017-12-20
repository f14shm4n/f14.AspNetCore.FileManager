using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface IFolderStructHandler : IOperationHandler<FolderStructParam, FolderStructResult>, IJOperationHandler<FolderStructParam, FolderStructResult>
    {

    }

    public class FolderStructHandler : BaseOperationHandler<FolderStructParam, FolderStructResult>, IFolderStructHandler
    {
        public FolderStructHandler(IHostingEnvironment env) : base(new FolderStructResult(), env.WebRootPath)
        {
        }

        public override FolderStructResult Run(FolderStructParam param)
        {
            ExHelper.NotNull(() => param);

            if (param.FileExtensions == null)
            {
                param.FileExtensions = new string[0];
            }

            string fullPath = PathHelper.GetFullPath(RootPath, param.CurrentFolderPath);

            if (Directory.Exists(fullPath))
            {
                try
                {
                    var dir = new DirectoryInfo(fullPath);
                    var files = (param.FileExtensions != null && param.FileExtensions.Length > 0 ? dir.GetFilesByExtensions(param.FileExtensions) : dir.EnumerateFiles())
                        .Select(x => new FileInfoProxy { Name = x.Name }).ToList();
                    var folders = dir.EnumerateDirectories().Select(x => new FileInfoProxy { Name = x.Name }).ToList();

                    Result.Folders = folders;
                    Result.Files = files;
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
