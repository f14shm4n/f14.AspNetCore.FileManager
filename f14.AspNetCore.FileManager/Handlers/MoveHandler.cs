using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// A short interface wrapper for the move action to use in service collection.
    /// </summary>
    public interface IMoveHandler : IOperationHandler<MoveParam, MoveResult>, IJOperationHandler<MoveParam, MoveResult>
    {

    }
    /// <summary>
    /// The move action implementation.
    /// </summary>
    public sealed class MoveHandler : BaseMoveHandler<MoveParam, MoveResult>, IMoveHandler
    {
        /// <summary>
        /// Creates new instance of move handler.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public MoveHandler(IHostingEnvironment env) : base(new MoveResult(), env.WebRootPath)
        {
        }
        /// <summary>
        /// Performs an action to move files.
        /// </summary>
        /// <param name="t">Single IO target.</param>
        /// <param name="srcObjPath">Path to source object.</param>
        /// <param name="dstObjPath">Path to destination object.</param>
        /// <param name="overwrite">Overwrite end point object or not.</param>
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
