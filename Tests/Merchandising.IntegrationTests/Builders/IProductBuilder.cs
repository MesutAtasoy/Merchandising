using Merchandising.IntegrationTests.Models.Categories;

namespace Merchandising.IntegrationTests.Builders;

public interface IProductBuilder
{
    IProductBuilder WithCategory(CategoryDto categoryDto);
    Task<ProductContext> BuildAsync();
}