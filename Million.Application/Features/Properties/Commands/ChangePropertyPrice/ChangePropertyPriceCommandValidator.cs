using FluentValidation;

namespace Million.Application.Features.Properties.Commands.ChangePropertyPrice
{
    /// <summary>
    /// Validator for ChangePropertyPriceCommand
    /// </summary>
    public class ChangePropertyPriceCommandValidator : AbstractValidator<ChangePropertyPriceCommand>
    {
        public ChangePropertyPriceCommandValidator()
        {
            RuleFor(x => x.NewPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be a positive value.");

            RuleFor(x => x.PropertyId)
                .GreaterThan(0)
                .WithMessage("Property ID must be a valid positive number.");
        }
    }
}