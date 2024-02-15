using Framework.Data.Pagination;
using MediatR;
using Merchandising.Application.Products.Dto;

namespace Merchandising.Application.Products.Queries.GetLiveProducts;

public class GetLiveProductsQuery: PaginationFilter, IRequest<PagedList<ProductDto>>
{
    public string Keyword { get; set; }
    public int? MinStockQuantity { get; set; }
    public int? MaxStockQuantity { get; set; }
}