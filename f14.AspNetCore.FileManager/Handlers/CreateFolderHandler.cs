using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// A short interface wrapper for the create folder action to use in service collection.
    /// </summary>
    public interface ICreateFolderHandler : IOperationHandler<CreateFolderParam, CreateFolderResult>, IJOperationHandler<CreateFolderParam, CreateFolderResult>
    {

    }
    /// <summary>
    /// The create folder action implementation.
    /// </summary>
    public sealed class CreateFolderHandler : BaseOperationHandler<CreateFolderParam, CreateFolderResult>, ICreateFolderHandler
    {
        /// <summary>
        /// Creates new instance of create folder handler.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public CreateFolderHandler(IHostingEnvironment env) : base(new CreateFolderResult(), env.WebRootPath)
        {
        }
        /// <summary>
        /// Run create folder action.
        /// </summary>
        /// <param name="param">Create folder param.</param>
        /// <returns>Create folder result.</returns>
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
