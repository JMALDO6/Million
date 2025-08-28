using MediatR;

namespace Million.Application.Features.Properties.Commands.ChangePropertyPrice
{
    /// <summary>
    /// Change Property Price Command
    /// </summary>
    public class ChangePropertyPriceCommand(int propertyId, decimal newPrice) : IRequest
    {
        public int PropertyId { get; set; } = propertyId;
        public decimal NewPrice { get; set; } = newPrice;
    }
}
