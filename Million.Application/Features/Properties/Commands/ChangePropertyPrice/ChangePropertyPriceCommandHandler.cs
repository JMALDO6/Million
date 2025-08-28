using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.Application.Common.Exceptions;
using Million.Application.Interfaces.Repositories;

namespace Million.Application.Features.Properties.Commands.ChangePropertyPrice
{
    /// <summary>
    /// Handler for changing the price of a property.
    /// </summary>
    /// <remarks>
    /// Ctor
    /// </remarks>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public class ChangePropertyPriceCommandHandler(IPropertyRepository repository, ILogger<ChangePropertyPriceCommandHandler> logger) : IRequestHandler<ChangePropertyPriceCommand>
    {
        private readonly IPropertyRepository _repository = repository;
        private readonly ILogger<ChangePropertyPriceCommandHandler> _logger = logger;

        public async Task Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Changing price for property ID: {PropertyId} to new price: {NewPrice}", request.PropertyId, request.NewPrice);

                var validator = new ChangePropertyPriceCommandValidator();
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );

                    _logger.LogWarning("Validation failed for ChangePropertyPriceCommand: {Errors}", errors);
                    throw new ValidationException(errors);
                }

                var property = await _repository.GetByIdAsync(request.PropertyId);
                if (property == null)
                {
                    _logger.LogWarning("Property with ID: {PropertyId} not found", request.PropertyId);
                    throw new NotFoundException(nameof(property), request.PropertyId);
                }

                property.Price = request.NewPrice;
                await _repository.UpdateAsync(property);
                _logger.LogInformation("Price updated successfully for property ID: {PropertyId}", request.PropertyId);
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException("An error occurred while accessing the database.", ex);
            }
        }
    }
}