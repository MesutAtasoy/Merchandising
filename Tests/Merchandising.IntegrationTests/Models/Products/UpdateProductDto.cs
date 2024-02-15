namespace Merchandising.IntegrationTests.Models.Products;

public class UpdateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
    public Guid? CategoryId { get; set; }
}