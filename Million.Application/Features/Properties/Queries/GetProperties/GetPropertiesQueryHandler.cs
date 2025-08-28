using MediatR;
using Microsoft.Extensions.Logging;
using Million.Application.Features.Properties.DTOs;
using Million.Application.Interfaces.Repositories;

namespace Million.Application.Features.Properties.Queries.GetProperties
{
    /// <summary>
    /// Handler for processing GetPropertiesQuery requests.
    /// </summary>
    public class GetPropertiesQueryHandler : IRequestHandler<GetPropertiesQuery, List<PropertyListItemDto>>
    {
        private readonly IPropertyRepository _repository;
        private readonly ILogger<GetPropertiesQueryHandler> _logger;

        /// <summary>
        /// Constructor for GetPropertiesQueryHandler.
        /// </summary>
        /// <param name="repository"></param>
        public GetPropertiesQueryHandler(IPropertyRepository repository, ILogger<GetPropertiesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<PropertyListItemDto>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetPropertiesQuery with filters: {@Filters}", request.Filters);
            var filters = request.Filters;
            var properties = await _repository.GetByFiltersAsync(filters);
            _logger.LogInformation("Retrieved {Count} properties matching the filters", properties.Count);

            return properties;
        }
    }
}
