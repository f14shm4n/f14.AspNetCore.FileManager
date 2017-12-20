using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface ICreateFolderHandler : IOperationHandler<CreateFolderParam, CreateFolderResult>, IJOperationHandler<CreateFolderParam, CreateFolderResult>
    {

    }

    public sealed class CreateFolderHandler : BaseOperationHandler<CreateFolderParam, CreateFolderResult>, ICreateFolderHandler
    {
        public CreateFolderHandler(IHostingEnvironment env) : base(new CreateFolderResult(), env.WebRootPath)
        {
        }

        public override CreateFolderResult Run(CreateFolderParam param)
        {
            ExHelper.NotNull(() => param);

            string fullPath = PathHelper.GetFullPath(RootPath, param.CurrentFolderPath);

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
