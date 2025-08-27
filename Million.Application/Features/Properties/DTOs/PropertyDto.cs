namespace Million.Application.Features.Properties.DTOs
{
    public class PropertyDto
    {
        /// <summary>
        /// Identifier of the property.
        /// </summary>
        public required string PropertyId { get; set; }

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
        /// Name of the property owner.
        /// </summary>
        public required string Owner { get; set; }
    }
}
