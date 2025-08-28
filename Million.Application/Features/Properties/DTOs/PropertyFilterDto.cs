namespace Million.Application.Features.Properties.DTOs
{
    /// <summary>
    /// Property filter Data Transfer Object
    /// </summary>
    public class PropertyFilterDto
    {
        /// <summary>
        /// Identifier of the property to filter by.
        /// </summary>
        public int? IdProperty { get; set; }

        /// <summary>
        /// Name of the property to filter by.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Address of the property to filter by.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Code internal of the property to filter by.
        /// </summary>
        public string? CodeInternal { get; set; }

        /// <summary>
        /// Minimum price of the property to filter by.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Maximum price of the property to filter by.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Owner name of the property to filter by.
        /// </summary>
        public string? OwnerName { get; set; }

        /// <summary>
        /// Owner ID of the property to filter by.
        /// </summary>
        public int? IdOwner { get; set; }

        /// <summary>
        /// Year of the property to filter by.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Page number for pagination (default is 1).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Page size for pagination (default is 10).
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
