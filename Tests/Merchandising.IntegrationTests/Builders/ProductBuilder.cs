using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using AutoFixture;
using FluentAssertions;
using Merchandising.IntegrationTests.Models.Categories;
using Merchandising.IntegrationTests.Models.Products;
using Microsoft.Extensions.Configuration;

namespace Merchandising.IntegrationTests.Builders;

public class ProductBuilder : IProductBuilder
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly Fixture _fixture = new();
    private CreateProductDto _createProductDto;

    public ProductBuilder(IConfiguration configuration)
    {
        _configuration = configuration;
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(_configuration["MerchandisingApi:BaseUri"]);
        _createProductDto = _fixture.Build<CreateProductDto>()
            .Without(x => x.CategoryId)
            .Create();
    }

    public IProductBuilder WithCategory(CategoryDto categoryDto)
    {
        _createProductDto.CategoryId = categoryDto.Id;

        return this;
    }

    public async Task<ProductContext> BuildAsync()
    {
        var content = new StringContent(JsonSerializer.Serialize(_createProductDto), Encoding.UTF8,
            MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync("/api/products", content);

        response.Should().NotBeNull();
        response.IsSuccessStatusCode.Should().BeTrue();

        var product = await response.Content.ReadFromJsonAsync<ProductDto>();

        return new ProductContext { Product = product };
    }
}