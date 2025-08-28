using MediatR;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Commands.UpdateProperty
{
    /// <summary>
    /// Command to update an existing property
    /// </summary>
    public class UpdatePropertyCommand(int propertyId, UpdatePropertyDto data) : IRequest<UpdatePropertyDto>
    {
        public int PropertyId { get; set; } = propertyId;
        public UpdatePropertyDto Property { get; set; } = data;
    }
}
