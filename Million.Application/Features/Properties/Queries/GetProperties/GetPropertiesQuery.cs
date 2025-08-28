using MediatR;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Queries.GetProperties
{
    /// <summary>
    /// Get properties query with filters
    /// </summary>
    public class GetPropertiesQuery(PropertyFilterDto filters) : IRequest<List<PropertyListItemDto>>
    {
        public PropertyFilterDto Filters { get; } = filters;
    }
}
