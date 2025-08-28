namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// Property list item Data Transfer Object
    /// </summary>
    public class PropertyListItemDto
    {
        /// <summary>
        /// Id of the property
        /// </summary>
        public int IdProperty { get; set; }

        /// <summary>
        /// Name of the property
        /// </summary>
        public required string Name { get; set; }

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
        /// Owner name of the property
        /// </summary>
        public required string OwnerName { get; set; }
    }
}
