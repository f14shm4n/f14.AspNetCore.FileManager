using MediatR;

namespace f14.AspNetCore.FileManager.Abstractions
{
    /// <summary>
    /// Represents the basic command model for managing files.
    /// </summary>
    public abstract class FileManagerCommand : IFileManagerRequest, IRequest
    {
        ///<inheritdoc/>
        public string CurrentFolderPath { get; set; } = "/";
    }
}
