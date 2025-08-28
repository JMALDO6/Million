using System.ComponentModel.DataAnnotations;

namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// Change Property Price Data Transfer Object
    /// </summary>
    public class ChangePropertyPriceDto
    {
        /// <summary>
        /// New Price of the Property
        /// </summary>
        [Required]
        public decimal NewPrice { get; set; }
    }
}
