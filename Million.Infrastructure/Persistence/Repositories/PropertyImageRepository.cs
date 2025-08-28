using Microsoft.Extensions.Logging;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Provides implementation for property image repository operations.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public class PropertyImageRepository(AppDbContext context, ILogger<PropertyImageRepository> logger) : IPropertyImageRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<PropertyImageRepository> _logger = logger;

        /// <inheritdoc/>
        public async Task AddAsync(PropertyImage image)
        {
            _logger.LogInformation("Adding a new image to property with ID: {IdProperty}", image.IdProperty);
            await _context.PropertyImages.AddAsync(image);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Image added with ID: {IdPropertyImage}", image.IdPropertyImage);
        }
    }
}
