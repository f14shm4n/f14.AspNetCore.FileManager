namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Represents the result model for the <see cref="CreateCommand"/>.
    /// </summary>
    public enum CreateCommandResult
    {
        /// <summary>
        /// Indicates that a new object has been created.
        /// </summary>            
        Created,
        /// <summary>
        /// Indicates that a object with the specified name already exists.
        /// </summary>
        Exists
    }
}
