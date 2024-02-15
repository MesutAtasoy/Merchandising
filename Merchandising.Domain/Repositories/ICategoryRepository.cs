using Merchandising.Domain.Entities;

namespace Merchandising.Domain.Repositories;

public interface ICategoryRepository
{
    /// <summary>
    /// Returns category
    /// </summary>
    /// <param name="id">The id of category</param>
    /// <returns></returns>
    Task<Category?> GetByIdAsync(Guid id);
}