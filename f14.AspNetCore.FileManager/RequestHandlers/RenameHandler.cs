using System;
using System.Collections.Generic;
using System.Text;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using System.IO;
using f14.IO;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    public class RenameHandler : IRequestHandler
    {
        public ResponseData Handle(RequestData requestData, ServiceVars vars)
        {
            var data = requestData as RenameRequestData;

            ExHelper.Require(data != null, $"Given request data must be a {nameof(RenameRequestData)} type.");
            ExHelper.Require(data.Renames.Count > 0, "No objects to rename.");

            string rootPath = vars.RootPath;
            string fullPath = PathHelper.GetFullPath(rootPath, data.Path);

            ExHelper.Require(Directory.Exists(fullPath), "Source directory does not exists.");

            List<string> errors = new List<string>();
            int affected = 0;
            foreach (var rfi in data.Renames)
            {
                string srcPath = Path.Combine(fullPath, rfi.OldName);
                string dstPath = Path.Combine(fullPath, rfi.NewName);

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
                    affected++;
                }
                catch (Exception ex)
                {
                    errors.Add($"Rename failed. Name info: {rfi.OldName} -> {rfi.NewName}. Ex: {ex.Message}");
                }
            }

            var result = new RenameResponseData
            {
                Errors = errors,
                Affected = affected
            };
            return result;
        }
    }
}
