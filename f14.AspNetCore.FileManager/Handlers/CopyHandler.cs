using f14.AspNetCore.FileManager.Data.Params;
using System;
using System.Collections.Generic;
using System.Text;
using f14.AspNetCore.FileManager.Data.Results;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using f14.IO;
using f14.AspNetCore.FileManager.Data;

namespace f14.AspNetCore.FileManager.Handlers
{
    public class CopyHandler : BaseMoveHandler<CopyParam>
    {
        public CopyHandler(IHostingEnvironment env) : base(new CopyResult(), env)
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
