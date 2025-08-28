namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// Property Image Data Transfer Object
    /// </summary>
    public class PropertyImageDto
    {
        /// <summary>
        /// Identifier of the property image.
        /// </summary>
        public int IdPropertyImage { get; set; }

        /// <summary>
        /// Identifier of the property to which the image belongs.
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Enabled status of the property image.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
