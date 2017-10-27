using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    public class DeleteHandler : IRequestHandler
    {
        public ResponseData Handle(RequestData requestData, ServiceVars vars)
        {
            var rData = requestData as DeleteRequestData;

            ExHelper.Require(rData != null, $"Given request data must be a {nameof(DeleteRequestData)} type.");            
            ExHelper.Require(!string.Equals(vars.RootPath, rData.Path, StringComparison.OrdinalIgnoreCase), "Cannot delete this folder.");

            string fullPath = PathHelper.GetFullPath(vars.RootPath, rData.Path);

            ExHelper.Require(!Path.HasExtension(fullPath), "The given path must be a folder path not a file path.");

            List<string> errors = new List<string>();

            int deleted = 0;            
            foreach (var n in rData.ObjectNames)
            {
                string target = Path.Combine(fullPath, n);
                try
                {
                    if (File.Exists(target))
                    {
                        File.Delete(target);
                        deleted++;
                    }
                    else if (Directory.Exists(target))
                    {
                        Directory.Delete(target, true);
                        deleted++;
                    }
                }
                catch(Exception ex)
                {
                    errors.Add($"Delete failed. Object name: {n} Message: {ex.Message}");
                }
            }

            var result = new DeleteResponseData
            {
                Affected = deleted,
                Errors = errors
            };

            return result;
        }
    }
}
