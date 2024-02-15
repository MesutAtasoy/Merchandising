using AutoFixture;
using Merchandising.IntegrationTests.Builders;
using Merchandising.IntegrationTests.Models;
using Merchandising.IntegrationTests.Models.Products;

namespace Merchandising.IntegrationTests.Steps;

[Binding]
public class ProductBuilderStepDefinitions
{
    private readonly IProductBuilder _productBuilder;
    private readonly SharedContext _sharedContext;
    private readonly Fixture _fixture = new();
    
    public ProductBuilderStepDefinitions(IProductBuilder productBuilder, SharedContext sharedContext)
    {
        _productBuilder = productBuilder;
        _sharedContext = sharedContext;
    }

    [Given(@"a random defined product")]
    public async Task CreateARandomProduct()
    {
        var product = await _productBuilder.BuildAsync();
        
        _sharedContext.TryAdd(nameof(ProductContext), product);
    }
    
    [Given(@"a random undefined product")]
    public async Task CreateARandomUndefineProduct()
    {
        var product = _fixture.Create<ProductDto>();
        _sharedContext.TryAdd(nameof(ProductContext), new ProductContext { Product = product});
    }
}