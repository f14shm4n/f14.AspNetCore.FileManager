using f14.AspNetCore.FileManager.Abstractions;
using FluentValidation;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Provides a validator for <see cref="MoveCommand"/>.
    /// </summary>
    public sealed class MoveCommandValidator : FileManagerRequestValidator<MoveCommand>
    {
        ///<inheritdoc/>
        public MoveCommandValidator()
        {
            RuleFor(x => x.SourceDirectory).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
