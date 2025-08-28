using Million.Application.Features.Properties.DTOs;
using Million.Domain.Entities;

namespace Million.Application.Interfaces.Repositories
{
    /// <summary>
    /// Provides an interface for property repository operations.
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// Adds a new property to the repository.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        Task AddAsync(Property property);

        /// <summary>
        /// Gets a property by its unique identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Property?> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing property in the repository.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        Task UpdateAsync(Property property);

        /// <summary>
        /// Gets a property by several filters.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<PropertyListItemDto>> GetByFiltersAsync(PropertyFilterDto filter);
        
        /// <summary>
        /// Checks if a property exists matching the given filters.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(PropertyFilterDto filter);
    }
}