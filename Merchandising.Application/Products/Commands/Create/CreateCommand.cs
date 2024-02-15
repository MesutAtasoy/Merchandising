using MediatR;
using Merchandising.Application.Products.Dto;

namespace Merchandising.Application.Products.Commands.Create;

public record CreateCommand : IRequest<ProductDto>
{
    public Guid? CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
}