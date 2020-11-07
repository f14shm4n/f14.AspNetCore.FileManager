using f14.AspNetCore.FileManager.Abstractions;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents the command model for the creating file or folder action.
    /// </summary>
    public sealed class CreateCommand : FileManagerCommand<CreateCommandResult>
    {
        /// <summary>
        /// Sets or gets the name of the folder to create.
        /// </summary>
        public string Name { get; set; } = default!;
    }
}
