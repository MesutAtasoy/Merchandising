namespace Merchandising.Domain.Entities;

public sealed class Category
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public int MinStockQuantity { get; private set; }
    public ICollection<Product> Products { get; private set; }

    private Category()
    {
        Id = Guid.NewGuid();
        Products = new List<Product>();
    }

    public static Category Create(Guid id, string name, int minStockQuantity)
    {
        return new Category
        {
            Id = id,
            Name = name,
            MinStockQuantity = minStockQuantity
        };
    }
}