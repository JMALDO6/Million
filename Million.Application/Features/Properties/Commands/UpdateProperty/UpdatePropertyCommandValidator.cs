using FluentValidation;

namespace Million.Application.Features.Properties.Commands.UpdateProperty
{
    /// <summary>
    /// Creates a validator for the CreatePropertyCommand.
    /// </summary>
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        /// <summary>
        /// Constructor that sets up the validation rules.
        /// </summary>
        public UpdatePropertyCommandValidator()
        {
            RuleFor(x => x.Property).SetValidator(new UpdatePropertyDtoValidator());
        }
    }
}
