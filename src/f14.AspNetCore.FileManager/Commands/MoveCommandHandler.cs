﻿using f14.IO;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents a command handler for <see cref="MoveCommand"/>.
    /// </summary>
    public sealed class MoveCommandHandler : AsyncRequestHandler<MoveCommand>
    {
        private readonly IWebHostEnvironment _env;

        /// <summary>
        /// Creates new instance of the handler.
        /// </summary>
        /// <param name="env">Hosting env.</param>
        public MoveCommandHandler(IWebHostEnvironment env)
        {
            _env = env;
        }

        ///<inheritdoc/>
        protected override Task Handle(MoveCommand command, CancellationToken cancellationToken)
        {
            string srcDirPath = PathHelper.GetFullPath(_env.WebRootPath, command.SourceDirectory);
            string dstDirPath = PathHelper.GetFullPath(_env.WebRootPath, command.CurrentFolderPath);

            ExceptionHelper.ThrowIfDirectoryNotExists(srcDirPath);
            ExceptionHelper.ThrowIfDirectoryNotExists(dstDirPath);

            string srcObjPath = PathHelper.GetFullPath(srcDirPath, command.Name);
            string dstObjPath = PathHelper.GetFullPath(dstDirPath, command.Name);

            if (command.IsFile)
            {
                File.Move(srcObjPath, dstObjPath, command.Overwrite);
            }
            else
            {
                FileIO.CopyAll(srcObjPath, dstObjPath, command.Overwrite);
                // Remove folder because the move folder method does not exists by default.
                Directory.Delete(srcObjPath, true);
            }

            return Task.FromResult(true);
        }
    }
}
