using Microsoft.Extensions.Logging;
using Million.Application.Interfaces.Repositories;
using Million.Domain.Entities;

namespace Million.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Provides implementation for property repository operations.
    /// </summary>
    /// <remarks>
    /// Constructor for PropertyRepository.
    /// </remarks>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public class PropertyRepository(AppDbContext context, ILogger<PropertyRepository> logger) : IPropertyRepository
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<PropertyRepository> _logger = logger;

        /// <inheritdoc/>
        public async Task AddAsync(Property property)
        {
            _logger.LogInformation("Adding a new property with address: {PropertyName}", property.Name);
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Property added with ID: {Id}", property.IdProperty);
        }

        /// <inheritdoc/>
        public async Task<Property?> GetByIdAsync(Guid id)
        {
            return await _context.Properties.FindAsync(id);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }
    }
}
