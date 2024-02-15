using Merchandising.IntegrationTests.Models.Categories;

namespace Merchandising.IntegrationTests.Models.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get;  set; }
    public string Description { get;  set; }
    public int StockQuantity { get;  set; }
    public CategoryDto? Category { get;  set; }
    public DateTime CreatedDate { get;  set; }
    public DateTime ModifiedDate { get;  set; }
}