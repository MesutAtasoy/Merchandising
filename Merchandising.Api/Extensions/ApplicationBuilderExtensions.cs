using Merchandising.Infrastructure;

namespace Merchandising.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureDbCreated(this WebApplication application)
    {
        using var serviceScope = application.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<MerchandisingDbContext>();
        dbContext.Database.EnsureCreated();
    }
}