using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// A short interface wrapper for the delete action to use in service collection.
    /// </summary>
    public interface IDeleteHandler : IOperationHandler<DeleteParam, DeleteResult>, IJOperationHandler<DeleteParam, DeleteResult>
    {

    }
    /// <summary>
    /// The delete action implementation.
    /// </summary>
    public sealed class DeleteHandler : BaseOperationHandler<DeleteParam, DeleteResult>, IDeleteHandler
    {
        /// <summary>
        /// Creates new instance of delete handler.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public DeleteHandler(IHostingEnvironment env) : base(new DeleteResult(), env.WebRootPath)
        {
        }
        /// <summary>
        /// Run delete action.
        /// </summary>
        /// <param name="param">Delete param.</param>
        /// <returns>Delete result.</returns>
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
