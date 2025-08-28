using FluentValidation;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Commands.AddPropertyImage
{
    /// <summary>
    /// AddPropertyImageDtoValidator is responsible for validating the AddPropertyImageDto.
    /// </summary>
    public class AddPropertyImageDtoValidator : AbstractValidator<AddPropertyImageDto>
    {
        public AddPropertyImageDtoValidator()
        {
            RuleFor(x => x.ImageFile)
                .NotNull().WithMessage("Image file is required.")
                .Must(file => file.Length <= 1_000_000)
                    .WithMessage("The image exceeds the maximum allowed size of 1 MB.")
                .Must(file => file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                    .WithMessage("Only PNG and JPEG images are allowed.");
        }
    }
}