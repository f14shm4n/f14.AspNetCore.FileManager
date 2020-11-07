using f14.AspNetCore.FileManager.Abstractions;
using FluentValidation;

namespace f14.AspNetCore.FileManager.Commands
{
    /// <summary>
    /// Provides a model validator for <see cref="CreateCommand"/>.
    /// </summary>
    public sealed class CreateCommandValidator : FileManagerRequestValidator<CreateCommand>
    {
        ///<inheritdoc/>
        public CreateCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
