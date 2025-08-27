using FluentValidation;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    /// <summary>
    /// Creates a validator for the CreatePropertyDto using FluentValidation.
    /// </summary>
    public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("The address is required.")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("The price must be greater than zero.");

            RuleFor(x => x.IdOwner)
                .GreaterThan(0).WithMessage("The owner ID must be greater than zero.");

            RuleFor(x => x.CodeInternal)
                .NotEmpty().WithMessage("The code internal is required.")
                .MaximumLength(50);

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage($"The year must be between 1900 and {DateTime.Now.Year}.");
        }
    }
}