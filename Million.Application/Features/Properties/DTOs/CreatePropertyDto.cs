namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// DTO for creating a new property.
    /// </summary>
    public class CreatePropertyDto
    {
        /// <summary>
        /// Address of the property.
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Name of the property.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// CodeInternal is a unique internal code for the property.
        /// </summary>
        public required string CodeInternal { get; set; }

        /// <summary>
        /// Price of the property.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Year the property was built.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// ID of the owner of the property.
        /// </summary>
        public int IdOwner { get; set; }
    }
}
