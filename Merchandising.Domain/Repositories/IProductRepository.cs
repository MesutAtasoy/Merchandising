using Merchandising.Domain.Entities;

namespace Merchandising.Domain.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Returns IQueryable
    /// </summary>
    /// <returns>IQueryable<Product?></returns>
    IQueryable<Product?> Get();
    
    /// <summary>
    /// Returns Product by id
    /// </summary>
    /// <param name="id">The id of product</param>
    /// <returns>Product</returns>
    Task<Product?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Adds new product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Added product</returns>
    Task<Product?> AddAsync(Product? product);
    
    /// <summary>
    /// Updates existing product
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Updated product</returns>
    Task<Product?> UpdateAsync(Product? product);
}