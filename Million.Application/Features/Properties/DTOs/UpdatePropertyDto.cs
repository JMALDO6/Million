namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// Update Property Data Transfer Object
    /// </summary>
    public class UpdatePropertyDto
    {
        /// <summary>
        /// Name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Address of the property
        /// </summary>
        public required string Address { get; set; }

        /// <summary>
        /// Price of the property
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Code internal of the property
        /// </summary>
        public required string CodeInternal { get; set; }

        /// <summary>
        /// Year of the property
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// ID of the owner of the property
        /// </summary>
        public int IdOwner { get; set; }
    }
}
