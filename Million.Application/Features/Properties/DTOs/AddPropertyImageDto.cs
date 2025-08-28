using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// DTO for adding an image to a property.
    /// </summary>
    public class AddPropertyImageDto
    {
        /// <summary>
        /// Image file to be uploaded.
        /// </summary>
        [Required]
        public required IFormFile ImageFile { get; set; }

        /// <summary>
        /// Enables or disables the image.
        /// </summary>
        [Required]
        public bool Enabled { get; set; } = true;
    }
}
