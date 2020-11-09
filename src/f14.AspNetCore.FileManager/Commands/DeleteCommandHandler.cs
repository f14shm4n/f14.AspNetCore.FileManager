using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents a command handler for <see cref="DeleteCommand"/>.
    /// </summary>
    public sealed class DeleteCommandHandler : AsyncRequestHandler<DeleteCommand>
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Creates new instance of the handler.
        /// </summary>
        /// <param name="env">Hosting env.</param>
        public DeleteCommandHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        ///<inheritdoc/>
        protected override Task Handle(DeleteCommand command, CancellationToken cancellationToken)
        {
            string pathToWorkDir = PathHelper.GetFullPath(_env.WebRootPath, command.CurrentFolderPath);

            ExceptionHelper.ThrowIfDirectoryNotExists(pathToWorkDir);

            string targetObjPath = Path.Combine(pathToWorkDir, command.TargetName);

            if (command.IsFile)
            {
                File.Delete(targetObjPath);
            }
            else
            {
                Directory.Delete(targetObjPath, true);
            }

            return Task.FromResult(true);
        }
    }
}
