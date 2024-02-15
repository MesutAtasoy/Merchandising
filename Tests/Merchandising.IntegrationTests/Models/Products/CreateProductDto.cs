﻿namespace Merchandising.IntegrationTests.Models.Products;

public class CreateProductDto
{
    public Guid? CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int StockQuantity { get; set; }
}