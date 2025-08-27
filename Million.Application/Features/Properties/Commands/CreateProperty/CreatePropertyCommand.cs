using MediatR;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Commands.CreateProperty
{
    /// <summary>
    /// Command to create a new property.
    /// </summary>
    public class CreatePropertyCommand(CreatePropertyDto property) : IRequest<PropertyDto>
    {
        public CreatePropertyDto Property { get; set; } = property;
    }
}
