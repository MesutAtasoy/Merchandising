using Merchandising.Application.Categories.Dto;
using Merchandising.Domain.Entities;

namespace Merchandising.Application.Products.Dto;

public record ProductDto
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int StockQuantity { get; private set; }
    public CategoryDto? Category { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime ModifiedDate { get; private set; }

    public static ProductDto MapTo(Product? product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = CategoryDto.MapTo(product.Category),
            StockQuantity = product.StockQuantity,
            CreatedDate = product.CreatedDate,
            ModifiedDate = product.ModifiedDate
        };
    }
}