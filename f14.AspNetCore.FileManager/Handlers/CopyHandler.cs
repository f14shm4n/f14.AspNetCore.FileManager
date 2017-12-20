using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface ICopyHandler : IOperationHandler<CopyParam, CopyResult>, IJOperationHandler<CopyParam, CopyResult>
    {
    }

    public sealed class CopyHandler : BaseMoveHandler<CopyParam, CopyResult>, ICopyHandler
    {
        public CopyHandler(IHostingEnvironment env) : base(new CopyResult(), env.WebRootPath)
        {
        }

        protected override void OnProcess(BaseActionTarget t, string srcObjPath, string dstObjPath, bool overwrite)
        {
            if (t.IsFile)
            {
                File.Copy(srcObjPath, dstObjPath, overwrite);
            }
            else
            {
                FileIO.CopyAll(srcObjPath, dstObjPath, overwriteFiles: overwrite);
            }
        }
    }
}
