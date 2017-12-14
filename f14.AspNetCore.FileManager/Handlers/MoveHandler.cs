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
    public class MoveHandler : BaseMoveHandler<MoveParam>
    {
        public MoveHandler(IHostingEnvironment env) : base(new MoveResult(), env)
        {
        }

        protected override void OnProcess(BaseActionTarget t, string srcObjPath, string dstObjPath, bool overwrite)
        {
            if (t.IsFile)
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
            else
            {
                FileIO.CopyAll(srcObjPath, dstObjPath, overwrite);
                Directory.Delete(srcObjPath, true); // Remove folder because the move folder method does not exists by default.
            }
        }
    }
}
