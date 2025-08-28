using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Features.Properties.Queries.GetProperties;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    /// <summary>
    /// Handler for creating a new property.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public class CreatePropertyCommandHandler(IPropertyRepository repository, ILogger<CreatePropertyCommandHandler> logger) : IRequestHandler<CreatePropertyCommand, PropertyDto>
    {
        private readonly IPropertyRepository _repository = repository;
        private readonly ILogger<CreatePropertyCommandHandler> _logger = logger;

        public async Task<PropertyDto> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling CreatePropertyCommand for property: {PropertyName}", request.Property.Name);
                var dto = request.Property;

                var exists = await _repository.ExistsAsync(new PropertyFilterDto { CodeInternal = dto.CodeInternal });
                
                if (exists)
                {
                    throw new ValidationException(new Dictionary<string, string[]>
                    {
                        { "CodeInternal", new[] { "A property with this internal code already exists." } }
                    });
                }

                var property = new Property(dto.Name, dto.Address, dto.Price, dto.CodeInternal, dto.Year, dto.IdOwner);
                await _repository.AddAsync(property);

                _logger.LogInformation("Property created at {PropertyName} with ID {Id}", dto.Name, property.IdProperty);

                return new PropertyDto
                {
                    PropertyId = property.IdProperty.ToString(),
                    Address = property.Address,
                    Name = property.Name,
                    CodeInternal = property.CodeInternal,
                    Price = property.Price,
                    Year = property.Year,
                    Owner = property.IdOwner
                };
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while accessing the database.", ex);
            }
        }
    }
}
