using f14.AspNetCore.FileManager.Abstractions;
using f14.AspNetCore.FileManager.Data;
using f14.AspNetCore.FileManager.Data.Params;
using f14.AspNetCore.FileManager.Data.Results;
using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace f14.AspNetCore.FileManager.Handlers
{
    public interface IMoveHandler : IOperationHandler<MoveParam, MoveResult>, IJOperationHandler<MoveParam, MoveResult>
    {

    }

    public sealed class MoveHandler : BaseMoveHandler<MoveParam, MoveResult>, IMoveHandler
    {
        public MoveHandler(IHostingEnvironment env) : base(new MoveResult(), env.WebRootPath)
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
