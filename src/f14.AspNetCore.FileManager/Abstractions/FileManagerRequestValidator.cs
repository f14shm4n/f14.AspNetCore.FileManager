using FluentValidation;

namespace f14.AspNetCore.FileManager.Abstractions
{
    /// <summary>
    /// Provides base validator for <see cref="RequestBase"/>.
    /// </summary>
    public abstract class FileManagerRequestValidator<T> : AbstractValidator<T> where T : IFileManagerRequest
    {
        /// <summary>
        /// Creates new instance of the validator.
        /// </summary>
        protected FileManagerRequestValidator()
        {
            RuleFor(x => x.CurrentFolderPath).NotEmpty();
        }
    }
}
