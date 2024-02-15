using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Merchandising.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MerchandisingDbContext _dbContext;
    
    public ProductRepository(MerchandisingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Product?> Get()
    {
        return _dbContext.Products
            .Include(x=>x.Category)
            .AsQueryable();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products
            .Include(x=>x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Product?> AddAsync(Product? product)
    {
        await _dbContext.Products.AddAsync(product);

        return product;
    }

    public async Task<Product?> UpdateAsync(Product? product)
    {
        _dbContext.Products.Update(product);

        return product;
    }
}