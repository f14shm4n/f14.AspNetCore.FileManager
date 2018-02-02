using f14.IO;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace f14.AspNetCore.FileManager
{
    /// <summary>
    /// A short interface wrapper for the copy action to use in service collection.
    /// </summary>
    public interface ICopyHandler : IOperationHandler<CopyParam, CopyResult>, IJOperationHandler<CopyParam, CopyResult>
    {
    }
    /// <summary>
    /// The rename copy implementation.
    /// </summary>
    public sealed class CopyHandler : BaseMoveHandler<CopyParam, CopyResult>, ICopyHandler
    {
        /// <summary>
        /// Creates new instance of copy handler.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public CopyHandler(IHostingEnvironment env) : base(new CopyResult(), env.WebRootPath)
        {
        }
        /// <summary>
        /// Performs an action to copy files.
        /// </summary>
        /// <param name="t">Single IO target.</param>
        /// <param name="srcObjPath">Path to source object.</param>
        /// <param name="dstObjPath">Path to destination object.</param>
        /// <param name="overwrite">Overwrite end point object or not.</param>
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
