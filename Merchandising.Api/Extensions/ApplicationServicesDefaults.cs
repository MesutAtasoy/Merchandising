using Framework.Domain;
using MediatR;
using Merchandising.Application.Products.Commands.Create;
using Merchandising.Domain.Repositories;
using Merchandising.Infrastructure;
using Merchandising.Infrastructure.Domain;
using Merchandising.Infrastructure.Repositories;

namespace Merchandising.Api.Extensions;

public static class ApplicationServicesDefaults
{
    internal static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddDbContext<MerchandisingDbContext>();

        builder.Services.AddMediatR(typeof(CreateCommand).Assembly);


        return builder;
    }
}