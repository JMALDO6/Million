using MediatR;
using Million.Application.Features.Properties.DTOs;

namespace Million.Application.Features.Properties.Commands.AddPropertyImage
{
    /// <summary>
    /// Command to add an image to a property.
    /// </summary>
    /// <param name="propertyImage"></param>
    public class AddPropertyImageCommand(int idProperty, AddPropertyImageDto propertyImage) : IRequest<PropertyImageDto>
    {
        /// <summary>
        /// Id of the property to which the image will be added.
        /// </summary>
        public int IdProperty { get; } = idProperty;

        /// <summary>
        /// Property image data to be added.
        /// </summary>
        public AddPropertyImageDto PropertyImage { get; set; } = propertyImage;
    }
}
