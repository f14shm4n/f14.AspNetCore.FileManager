using f14.AspNetCore.FileManager.Abstractions;
using FluentValidation;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Provides a model validator for <see cref="DeleteCommand"/>.
    /// </summary>
    public sealed class DeleteCommandValidator : FileManagerRequestValidator<DeleteCommand>
    {
        ///<inheritdoc/>
        public DeleteCommandValidator()
        {
            RuleFor(x => x.TargetName).NotEmpty();
        }
    }
}
