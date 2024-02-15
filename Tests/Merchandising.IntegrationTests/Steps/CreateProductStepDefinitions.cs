using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Merchandising.IntegrationTests.Constants;
using Merchandising.IntegrationTests.Models;
using Merchandising.IntegrationTests.Models.Products;
using Microsoft.Extensions.Configuration;

namespace Merchandising.IntegrationTests.Steps;

[Binding]
public class CreateProductStepDefinitions
{
    private Guid categoryId = Guid.Parse("c365d08f-7cd4-4b6e-bae3-722bfd51e931");
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly Fixture _fixture;
    private readonly SharedContext _sharedContext;

    public CreateProductStepDefinitions(IConfiguration configuration,
        SharedContext sharedContext)
    {
        _configuration = configuration;
        _sharedContext = sharedContext;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration["MerchandisingApi:BaseUri"]);
        _fixture = new Fixture();
    }

    [When(@"I send the create product request")]
    public async Task WhenISendTheCreateProductRequest()
    {
        var createProduct = _fixture.Build<CreateProductDto>()
            .With(x => x.CategoryId, categoryId)
            .Create();

        var content = new StringContent(JsonSerializer.Serialize(createProduct), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
        _sharedContext.TryAdd(nameof(CreateProductDto), createProduct);
    }

    [When(@"I send the create product request without category")]
    public async Task WhenISendTheCreateProductRequestWithoutCategory()
    {
        var createProduct = _fixture.Build<CreateProductDto>()
            .Without(x => x.CategoryId)
            .Create();

        var content = new StringContent(JsonSerializer.Serialize(createProduct), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
        _sharedContext.TryAdd(nameof(CreateProductDto), createProduct);
    }


    [When(@"I send the create product request without name")]
    public async Task WhenISendTheCreateProductRequestWithoutName()
    {
        var createProduct = _fixture.Build<CreateProductDto>()
            .Without(x => x.CategoryId)
            .Without(x => x.Name)
            .Create();

        var content = new StringContent(JsonSerializer.Serialize(createProduct), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
        _sharedContext.TryAdd(nameof(CreateProductDto), createProduct);
    }
    
    [When(@"I send the create product request with (.*) character\(s\) name")]
    public async Task WhenISendTheCreateProductRequestWithCharacterSName(int characterCount)
    {
        var name = string.Join("", Enumerable.Repeat(0, characterCount).Select(n => (char)new Random().Next(127)));
        var createProduct = _fixture.Build<CreateProductDto>()
            .Without(x => x.CategoryId)
            .With(x => x.Name, name)
            .Create();

        var content = new StringContent(JsonSerializer.Serialize(createProduct), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
        _sharedContext.TryAdd(nameof(CreateProductDto), createProduct);
    }

    [Then(@"return valid created product")]
    public async Task ThenReturnValidCreatedProduct()
    {
        var response = _sharedContext.Get<HttpResponseMessage>(SharedContextConstants.Response);

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        var expectedProduct = _sharedContext.Get<CreateProductDto>(nameof(CreateProductDto));

        product.Should().NotBeNull();
        product.Id.Should().NotBeEmpty();
        product.Name.Should().Be(expectedProduct.Name);
        product.Description.Should().Be(expectedProduct.Description);
        product.StockQuantity.Should().Be(expectedProduct.StockQuantity);

        if (expectedProduct.CategoryId.HasValue)
        {
            product.Category.Id.Should().Be(expectedProduct.CategoryId.Value);
        }
    }

    [When(@"I send the create product request with unknown category")]
    public async Task WhenISendTheCreateProductRequestWithUnknownCategory()
    {
        var createProduct = _fixture.Build<CreateProductDto>()
            .With(x => x.CategoryId, _fixture.Create<Guid>())
            .Create();

        var content = new StringContent(JsonSerializer.Serialize(createProduct), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        _sharedContext.TryAdd(SharedContextConstants.Response, response);
        _sharedContext.TryAdd(nameof(CreateProductDto), createProduct);
    }
}