using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using System.IO;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    public class CreateFolderHandler : IRequestHandler
    {
        public ResponseData Handle(RequestData requestData, ServiceVars vars)
        {
            var rData = requestData as CreateRequestData;

            ExHelper.Require(rData != null, $"Given request data must be a {nameof(CreateRequestData)} type.");
            ExHelper.Require(!string.Equals(vars.RootPath, rData.Path, StringComparison.OrdinalIgnoreCase), "Cannot delete this folder.");

            string fullPath = PathHelper.GetFullPath(vars.RootPath, rData.Path);

            string fullPathToNewObject = Path.Combine(fullPath, rData.Name);

            ExHelper.Require(!Path.HasExtension(fullPathToNewObject), "The path to the created object must be a folder path not a file path.");

            List<string> errors = new List<string>();

            try
            {
                Directory.CreateDirectory(fullPathToNewObject);
            }
            catch (Exception ex)
            {
                errors.Add($"Unable to create folder. Ex: {ex.Message}");
            }

            return new ResponseData
            {
                Errors = errors
            };
        }
    }
}
