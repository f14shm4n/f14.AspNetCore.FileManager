using System;
using System.Collections.Generic;
using System.Text;
using f14.AspNetCore.FileManager.RequestModels;
using f14.AspNetCore.FileManager.ResponseModels;
using System.IO;
using f14.IO;

namespace f14.AspNetCore.FileManager.RequestHandlers
{
    public class MoveHandler : IRequestHandler
    {
        public ResponseData Handle(RequestData requestData, ServiceVars vars)
        {
            var data = requestData as MoveRequestData;

            ExHelper.Require(data != null, $"Request data must be a {nameof(MoveRequestData)} type.");

            ExHelper.Require(!string.IsNullOrWhiteSpace(data.SourceDirectory), "Source directory must be set.");
            ExHelper.Require(!string.IsNullOrWhiteSpace(data.DestinationDirectory), "Destination directory must be set.");
            ExHelper.Require(data.Targets.Count > 0, "Targets must be set.");

            bool isCopy = string.Equals(data.Type, "copy", StringComparison.OrdinalIgnoreCase);

            string srcDirPath = Path.Combine(vars.RootPath, data.SourceDirectory);
            string dstDirPath = Path.Combine(vars.RootPath, data.DestinationDirectory);

            List<string> errors = new List<string>();

            foreach (var t in data.Targets)
            {
                string srcObjPath = Path.Combine(srcDirPath, t.Name);
                string dstObjPath = Path.Combine(dstDirPath, t.Name);

                try
                {
                    if (isCopy)
                    {
                        if (t.IsFile)
                        {
                            File.Copy(srcObjPath, dstObjPath, data.Overwrite);
                        }
                        else
                        {
                            FileIO.CopyAll(srcObjPath, dstObjPath, overwriteFiles: data.Overwrite);
                        }
                    }
                    else
                    {
                        if (t.IsFile)
                        {
                            if (File.Exists(srcObjPath))
                            {
                                if (File.Exists(dstObjPath))
                                {
                                    File.Delete(dstObjPath);
                                    File.Move(srcObjPath, dstObjPath);
                                }
                                else
                                {
                                    File.Move(srcObjPath, dstObjPath);
                                }
                            }
                        }
                        else
                        {
                            FileIO.CopyAll(srcObjPath, dstObjPath, data.Overwrite);
                        }
                    }
                }
                catch
                {
                    errors.Add($"Move/Copy failed. Name info: {t.Name}");
                }
            }

            var result = new ResponseData
            {
                Errors = errors
            };   
            return result;
        }
    }
}
