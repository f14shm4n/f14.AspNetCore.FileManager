namespace f14.AspNetCore.FileManager.Abstractions
{
    /// <summary>
    /// Represents the basic command and query model for managing files.
    /// </summary>
    public interface IFileManagerRequest
    {
        /// <summary>
        /// Sets or gets a working directory path.
        /// <para>
        /// All actions with files will be performed in this folder.
        /// </para>
        /// </summary>
        string CurrentFolderPath { get; }
    }
}
