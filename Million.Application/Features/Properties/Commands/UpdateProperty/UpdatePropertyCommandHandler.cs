using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;

namespace Million.Application.Features.Properties.Commands.UpdateProperty
{
    /// <summary>
    /// Handler for updating an existing property
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="repository"></param>
    public class UpdatePropertyCommandHandler(IPropertyRepository repository, ILogger<UpdatePropertyCommandHandler> logger) : IRequestHandler<UpdatePropertyCommand, UpdatePropertyDto>
    {
        private readonly IPropertyRepository _repository = repository;
        private readonly ILogger<UpdatePropertyCommandHandler> _logger = logger;

        public async Task<UpdatePropertyDto> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating property with ID: {PropertyId}", request.PropertyId);
                var property = await _repository.GetByIdAsync(request.PropertyId);
                if (property == null)
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        { "PropertyId", new[] { "Property not found." } }
                    });
                }

                property.Name = request.Property.Name;
                property.Address = request.Property.Address;
                property.Price = request.Property.Price;
                property.CodeInternal = request.Property.CodeInternal;
                property.Year = request.Property.Year;
                property.IdOwner = request.Property.IdOwner;

                await _repository.UpdateAsync(property);
                _logger.LogInformation("Property with ID: {PropertyId} updated successfully", request.PropertyId);
                
                return request.Property;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while accessing the database.", ex);
            }
        }
    }
}