namespace Merchandising.Application.Products.Commands.Update.Request;

public record UpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid? CategoryId { get; set; }
}