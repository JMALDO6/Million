namespace Million.Application.Features.Properties.Queries.GetProperties
{
    /// <summary>
    /// Property filter criteria for querying properties.
    /// </summary>
    public class PropertyFilterDto
    {
        public string? Address { get; set; }
        public string? CodeInternal { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Year { get; set; }
        public int? IdOwner { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}