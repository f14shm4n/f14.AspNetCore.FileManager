using f14.AspNetCore.FileManager.Abstractions;
using FluentValidation;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Provides a validator for <see cref="RenameCommand"/>.
    /// </summary>
    public sealed class RenameCommandValidator : FileManagerRequestValidator<RenameCommand>
    {
        ///<inheritdoc/>
        public RenameCommandValidator()
        {
            RuleFor(x => x.OriginName).NotEmpty();
            RuleFor(x => x.NewName).NotEmpty().NotEqual(x => x.OriginName);
        }
    }
}
