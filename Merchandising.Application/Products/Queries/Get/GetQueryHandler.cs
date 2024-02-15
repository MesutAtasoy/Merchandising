using Framework.Data.Pagination;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Repositories;

namespace Merchandising.Application.Products.Queries.Get;

public class GetQueryHandler : IRequestHandler<GetQuery, PagedList<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedList<ProductDto>> Handle(GetQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Get();
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            query = query.Where(x =>
                string.Equals(x!.Name.ToLowerInvariant(), request.Keyword.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(x!.Description.ToLowerInvariant(), request.Keyword.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase) ||
                (x.Category != null && 
                 string.Equals(x!.Category.Name.ToLowerInvariant(), request.Keyword.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase)));
        }

        if (request.MinStockQuantity.HasValue)
        {
            query = query.Where(x => x.StockQuantity >= request.MinStockQuantity.Value);
        }

        if (request.MaxStockQuantity.HasValue)
        {
            query = query.Where(x => x.StockQuantity <= request.MaxStockQuantity.Value);
        }

        var products = await query
            .Select(x => ProductDto.MapTo(x))
            .ToPagedListAsync(request);

        return products;
    }
}