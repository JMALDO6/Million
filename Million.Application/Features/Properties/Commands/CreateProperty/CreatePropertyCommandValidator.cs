using FluentValidation;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    /// <summary>
    /// Creates a validator for the CreatePropertyCommand.
    /// </summary>
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        /// <summary>
        /// Constructor that sets up the validation rules.
        /// </summary>
        public CreatePropertyCommandValidator()
        {
            RuleFor(x => x.Property).SetValidator(new CreatePropertyDtoValidator());
        }
    }
}
