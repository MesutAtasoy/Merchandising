using AutoFixture;
using Merchandising.IntegrationTests.Builders;
using Merchandising.IntegrationTests.Constants;
using Merchandising.IntegrationTests.Models;
using Microsoft.Extensions.Configuration;

namespace Merchandising.IntegrationTests.Steps;

[Binding]
public class DeleteProductStepDefinitions
{
    
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly Fixture _fixture;
    private readonly SharedContext _sharedContext;

    public DeleteProductStepDefinitions(IConfiguration configuration,
        SharedContext sharedContext)
    {
        _configuration = configuration;
        _sharedContext = sharedContext;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration["MerchandisingApi:BaseUri"]);
        _fixture = new Fixture();
    }
    
    [When(@"I send the delete product request")]
    public async Task WhenISendTheDeleteProductRequest()
    {
        var productContext = _sharedContext.Get<ProductContext>(nameof(ProductContext));
   
        var response = await _httpClient.DeleteAsync($"/api/products/{productContext!.Product.Id}");

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
    }
}