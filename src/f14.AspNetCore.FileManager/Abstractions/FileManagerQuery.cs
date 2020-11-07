using MediatR;

namespace f14.AspNetCore.FileManager.Abstractions
{
    /// <summary>
    /// Represents the basic query model for managing files.
    /// </summary>
    public abstract class FileManagerQuery<T> : IFileManagerRequest, IRequest<T>
    {
        ///<inheritdoc/>
        public string CurrentFolderPath { get; set; } = "/";
    }
}
