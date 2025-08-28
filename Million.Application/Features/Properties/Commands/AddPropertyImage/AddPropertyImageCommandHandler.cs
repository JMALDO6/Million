using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;

namespace Million.Application.Features.Properties.Commands.AddPropertyImage
{
    /// <summary>
    /// Handler for adding an image to a property.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public class AddPropertyImageCommandHandler(IPropertyImageRepository repository, ILogger<AddPropertyImageCommandHandler> logger) : IRequestHandler<AddPropertyImageCommand, PropertyImageDto>
    {
        private readonly IPropertyImageRepository _repository = repository;
        private readonly ILogger<AddPropertyImageCommandHandler> _logger = logger;

        public async Task<PropertyImageDto> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling AddPropertyImageCommand for property ID: {IdProperty}", request.IdProperty);

                if (request.PropertyImage.ImageFile == null || request.PropertyImage.ImageFile.Length == 0)
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        { "ImageFile", new[] { "Image file is required." } }
                    });
                }

                using var memoryStream = new MemoryStream();
                await request.PropertyImage.ImageFile.CopyToAsync(memoryStream);

                var propertyImage = new PropertyImage
                {
                    IdProperty = request.IdProperty,
                    File = memoryStream.ToArray(),
                    Enabled = true
                };

                var image = await _repository.AddAsync(propertyImage);
                _logger.LogInformation("Image added to property ID {IdProperty} with Image ID {IdPropertyImage}", request.IdProperty, propertyImage.IdPropertyImage);

                return new PropertyImageDto
                {
                    IdPropertyImage = image.IdPropertyImage,
                    IdProperty = image.IdProperty,
                    Enabled = image.Enabled
                };
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while accessing the database.", ex);
            }
        }
    }
}