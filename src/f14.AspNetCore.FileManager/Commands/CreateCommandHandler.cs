using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents a command handler for <see cref="CreateCommand"/>.
    /// </summary>
    public sealed class CreateCommandHandler : IRequestHandler<CreateCommand, CreateCommandResult>
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Creates new instance of the handler.
        /// </summary>
        /// <param name="env">Hosting env.</param>
        public CreateCommandHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        ///<inheritdoc/>
        public Task<CreateCommandResult> Handle(CreateCommand command, CancellationToken cancellationToken)
        {
            ExceptionHelper.ThrowIfFileOrFolderNameIsInvalid(command.Name);

            string pathToWorkDir = PathHelper.GetFullPath(_env.WebRootPath, command.CurrentFolderPath);

            ExceptionHelper.ThrowIfDirectoryNotExists(pathToWorkDir);

            string pathToNewFolder = Path.Combine(pathToWorkDir, command.Name);
            CreateCommandResult result = CreateCommandResult.Exists;

            if (!Directory.Exists(pathToNewFolder))
            {
                Directory.CreateDirectory(pathToNewFolder);
                result = CreateCommandResult.Created;
            }

            return Task.FromResult(result);
        }
    }
}
