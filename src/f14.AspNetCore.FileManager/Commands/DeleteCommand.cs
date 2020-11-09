using f14.AspNetCore.FileManager.Abstractions;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents the command model for the deleting file or folder action.
    /// </summary>
    public sealed class DeleteCommand : FileManagerCommand
    {
        /// <summary>
        /// Sets or gets the name of the file or folder to delete.
        /// </summary>
        public string TargetName { get; set; } = default!;

        /// <summary>
        /// Indicates that the object is file or not.
        /// </summary>
        public bool IsFile { get; set; }
    }
}
