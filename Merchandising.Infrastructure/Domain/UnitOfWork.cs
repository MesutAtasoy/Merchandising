using Framework.Domain;

namespace Merchandising.Infrastructure.Domain;

public class UnitOfWork : IUnitOfWork
{
    private readonly MerchandisingDbContext _merchandisingDbContext;

    public UnitOfWork(MerchandisingDbContext merchandisingDbContext)
    {
        _merchandisingDbContext = merchandisingDbContext;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        return await _merchandisingDbContext.SaveChangesAsync(cancellationToken);
    }
}