using System;
using System.Collections.Generic;
using System.Text;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using System.IO;
using f14.IO;
using System.Linq;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    public class StructHandler : IRequestHandler
    {
        public ResponseData Handle(RequestData requestData, ServiceVars vars)
        {
            var rData = requestData as StructRequestData;

            ExHelper.Require(rData != null, $"Given request data must be a {nameof(StructRequestData)} type.");
            ExHelper.Require(!string.IsNullOrWhiteSpace(rData.Path) && !Path.HasExtension(rData.Path), "Folder path is empty or it is a file path.");

            if (rData.FileExtensions == null)
            {
                rData.FileExtensions = new string[0];
            }

            string fullPath = PathHelper.GetFullPath(vars.RootPath, rData.Path);

            ExHelper.Require(Directory.Exists(fullPath), "Folder path must point at to an existing folder.");

            try
            {
                var dir = new DirectoryInfo(fullPath);
                var files = (rData.FileExtensions != null && rData.FileExtensions.Length > 0 ? dir.GetFilesByExtensions(rData.FileExtensions) : dir.EnumerateFiles())
                    .Select(x => new FileInfoProxy { Name = x.Name }).ToList();
                var folders = dir.EnumerateDirectories().Select(x => new FileInfoProxy { Name = x.Name }).ToList();

                var result = new StructResponseData
                {
                    FolderCount = folders.Count,
                    Folders = folders,
                    FileCount = files.Count,
                    Files = files
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Cannot get folder for path: {rData.Path}.", ex);
            }
        }
    }
}
