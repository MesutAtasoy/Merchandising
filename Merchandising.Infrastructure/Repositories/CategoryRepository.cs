using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Merchandising.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly MerchandisingDbContext _dbContext;

    public CategoryRepository(MerchandisingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
}