 using Framework.Data.Pagination;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Application.Products.Specifications;
using Merchandising.Domain.Repositories;

namespace Merchandising.Application.Products.Queries.GetLiveProducts;

public class GetLiveProductsQueryHandler: IRequestHandler<GetLiveProductsQuery, PagedList<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetLiveProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<PagedList<ProductDto>> Handle(GetLiveProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productRepository.Get()
            .Where(new LiveProductSpecification()!);
        
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