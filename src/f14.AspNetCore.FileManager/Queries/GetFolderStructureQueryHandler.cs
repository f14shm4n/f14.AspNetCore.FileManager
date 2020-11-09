using f14.AspNetCore.FileManager.Extensions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Queries
{
    /// <summary>
    /// Represents a query handler for <see cref="GetFolderStructureQuery"/>.
    /// </summary>
    public sealed class GetFolderStructureQueryHandler : IRequestHandler<GetFolderStructureQuery, FolderStructureInfo>
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Creates new instance of the handler.
        /// </summary>
        /// <param name="env">Hosting env.</param>
        public GetFolderStructureQueryHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        ///<inheritdoc/>
        public Task<FolderStructureInfo> Handle(GetFolderStructureQuery query, CancellationToken cancellationToken)
        {
            string pathToWorkDir = PathHelper.GetFullPath(_env.WebRootPath, query.CurrentFolderPath);

            ExceptionHelper.ThrowIfDirectoryNotExists(pathToWorkDir);

            var dir = new DirectoryInfo(pathToWorkDir);
            var result = new FolderStructureInfo();

            if (query.FileExtensions?.Any() == true)
            {
                result.SetFiles(dir.GetFilesByExtensions(query.FileExtensions));
            }
            else
            {
                result.SetFiles(dir.EnumerateFiles());
            }

            result.SetFolders(dir.EnumerateDirectories());
            return Task.FromResult(result);
        }
    }
}

