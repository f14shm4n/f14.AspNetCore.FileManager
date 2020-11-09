using f14.AspNetCore.FileManager.Abstractions;
using FluentValidation;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Provides a validator for <see cref="CopyCommand"/>.
    /// </summary>
    public sealed class CopyCommandValidator : FileManagerRequestValidator<CopyCommand>
    {
        ///<inheritdoc/>
        public CopyCommandValidator()
        {
            RuleFor(x => x.SourceDirectory).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
