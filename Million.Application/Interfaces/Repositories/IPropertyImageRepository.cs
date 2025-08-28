using Million.Domain.Entities;

namespace Million.Application.Interfaces.Repositories
{
    /// <summary>
    /// Provides an interface for property image repository operations.
    /// </summary>
    public interface IPropertyImageRepository
    {
        /// <summary>
        /// Adds a new image to a property.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<PropertyImage> AddAsync(PropertyImage image);
    }
}
