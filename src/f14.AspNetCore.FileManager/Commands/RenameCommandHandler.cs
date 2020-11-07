using f14.IO;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents a command handler for <see cref="RenameCommand"/>.
    /// </summary>
    public sealed class RenameCommandHandler : AsyncRequestHandler<RenameCommand>
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Creates new instance of the handler.
        /// </summary>
        /// <param name="env">Hosting env.</param>
        public RenameCommandHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        ///<inheritdoc/>
        protected override Task Handle(RenameCommand command, CancellationToken cancellationToken)
        {
            ExceptionHelper.ThrowIfFileOrFolderNameIsInvalid(command.OriginName);
            ExceptionHelper.ThrowIfFileOrFolderNameIsInvalid(command.NewName);

            string pathToWorkDir = PathHelper.GetFullPath(_env.WebRootPath, command.CurrentFolderPath);

            ExceptionHelper.ThrowIfDirectoryNotExists(pathToWorkDir);

            string srcPath = Path.Combine(pathToWorkDir, command.OriginName);
            string dstPath = Path.Combine(pathToWorkDir, command.NewName);

            if (command.IsFile)
            {
                FileIO.RenameFile(srcPath, dstPath);
            }
            else
            {
                FileIO.RenameFolder(srcPath, dstPath);
            }

            return Task.FromResult(true);
        }
    }
}
