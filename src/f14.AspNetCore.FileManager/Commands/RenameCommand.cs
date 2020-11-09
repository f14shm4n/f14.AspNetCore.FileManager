using f14.AspNetCore.FileManager.Abstractions;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents the command model for the renaming file or folder action.
    /// </summary>
    public sealed class RenameCommand : FileManagerCommand
    {
        /// <summary>
        /// Sets or gets the object origin name.
        /// </summary>
        public string OriginName { get; set; } = default!;

        /// <summary>
        /// Sets or gets the object new name.
        /// </summary>
        public string NewName { get; set; } = default!;

        /// <summary>
        /// Indicates that the object is file or not.
        /// </summary>
        public bool IsFile { get; set; }
    }
}
