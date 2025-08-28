using FluentValidation;

namespace Million.Application.Features.Properties.Commands.AddPropertyImage
{
    /// <summary>
    /// AddPropertyImageCommandValidator is responsible for validating the AddPropertyImageCommand.
    /// </summary>
    public class AddPropertyImageCommandValidator : AbstractValidator<AddPropertyImageCommand>
    {
        /// <summary>
        /// Constructor that sets up the validation rules.
        /// </summary>
        public AddPropertyImageCommandValidator()
        {
            RuleFor(x => x.PropertyImage).SetValidator(new AddPropertyImageDtoValidator());
        }
    }
}
