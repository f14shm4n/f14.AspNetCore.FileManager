using MediatR;

namespace f14.AspNetCore.FileManager.Abstractions
{
    /// <summary>
    /// Represents the basic command model for managing files.
    /// </summary>
    /// <typeparam name="T">Type of the request result.</typeparam>
    public abstract class FileManagerCommand<T> : IFileManagerRequest, IRequest<T>
    {
        ///<inheritdoc/>
        public string CurrentFolderPath { get; set; } = "/";
    }
}
