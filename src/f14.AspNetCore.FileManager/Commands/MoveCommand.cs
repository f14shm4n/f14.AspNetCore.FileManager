using f14.AspNetCore.FileManager.Abstractions;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents the command model for the moving file or folder action.
    /// </summary>
    public sealed class MoveCommand : FileManagerCommand
    {
        /// <summary>
        /// Sets or gets source folder path.
        /// </summary>
        public string SourceDirectory { get; set; } = default!;

        /// <summary>
        /// Sets or gets the target file or folder name.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Indicates that the object is file or not.
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// Determines whether overwrite files or not.
        /// </summary>
        public bool Overwrite { get; set; } = false;
    }
}
