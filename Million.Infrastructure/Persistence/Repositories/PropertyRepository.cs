using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Million.Application.Features.Properties.Queries.GetProperties;
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
        public async Task<List<Property>> GetByFiltersAsync(PropertyFilterDto filter)
        {
            var query = _context.Properties.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Address))
                query = query.Where(p => p.Address.Contains(filter.Address));

            if (!string.IsNullOrWhiteSpace(filter.CodeInternal))
                query = query.Where(p => p.CodeInternal == filter.CodeInternal);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.Year.HasValue)
                query = query.Where(p => p.Year == filter.Year.Value);

            if (filter.IdOwner.HasValue)
                query = query.Where(p => p.IdOwner == filter.IdOwner.Value);

            return await query.Skip((filter.Page - 1) * filter.PageSize)
                        .Take(filter.PageSize)
                        .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(PropertyFilterDto filter)
        {
            var query = _context.Properties.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.Address))
                query = query.Where(p => p.Address.Contains(filter.Address));

            if (!string.IsNullOrWhiteSpace(filter.CodeInternal))
                query = query.Where(p => p.CodeInternal == filter.CodeInternal);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (filter.Year.HasValue)
                query = query.Where(p => p.Year == filter.Year.Value);

            if (filter.IdOwner.HasValue)
                query = query.Where(p => p.IdOwner == filter.IdOwner.Value);

            return await query.AnyAsync();
        }

        /// <inheritdoc/>
        public async Task<Property?> GetByIdAsync(int id)
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
