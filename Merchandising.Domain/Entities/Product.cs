using Framework.Domain;
using Merchandising.Domain.Rules.Products;

namespace Merchandising.Domain.Entities;

public sealed class Product : Entity
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int StockQuantity { get; private set; }
    public Guid? CategoryId { get; private set; }
    public Category? Category { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime ModifiedDate { get; private set; }
    public bool IsDeleted { get; private set; }

    private Product()
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
        CreatedDate = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Creates an instance of product entity 
    /// </summary>
    /// <param name="name">The name of product</param>
    /// <param name="description">The description of product</param>
    /// <param name="stockQuantity">The stock quantity of product</param>
    /// <param name="category">The category of product</param>
    /// <returns>Product</returns>
    public static Product? Create(string name,
        string description,
        int stockQuantity,
        Category? category)
    {
        CheckRule(new TitleRequiredRule(name));
        CheckRule(new TitleMaxCharacterRule(name));
        CheckRule(new StockQuantityMustBePositiveRule(stockQuantity));
        
        var product = new Product
        {
            Name = name,
            Category = category,
            CategoryId = category?.Id,
            Description = description,
            StockQuantity = stockQuantity
        };

        return product;
    }

    /// <summary>
    /// Updates an existing instance of product entity 
    /// </summary>
    /// <param name="name">The name of product</param>
    /// <param name="description">The description of product</param>
    /// <param name="stockQuantity">The stock quantity of product</param>
    /// <param name="category">The category of product</param>
    /// <returns>Product</returns>
    public Product Update(string name,
        string description,
        int stockQuantity,
        Category? category)
    {
        CheckRule(new TitleRequiredRule(name));
        CheckRule(new TitleMaxCharacterRule(name));
        CheckRule(new StockQuantityMustBePositiveRule(stockQuantity));
        
        Name = name;
        Category = category;
        CategoryId = category?.Id;
        Description = description;
        StockQuantity = stockQuantity;
        ModifiedDate = DateTime.UtcNow;

        return this;
    }

    /// <summary>
    /// Mark as deleted entity
    /// </summary>
    public void Delete()
    {
        IsDeleted = true;
        ModifiedDate = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"Id:{Id}, Name:{Name}";
    }
}