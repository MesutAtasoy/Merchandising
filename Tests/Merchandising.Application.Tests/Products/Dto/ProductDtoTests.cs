using AutoFixture;
using FluentAssertions;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Entities;

namespace Merchandising.Application.Tests.Products.Dto;

public class ProductDtoTests
{
    private readonly Fixture _fixture;

    public ProductDtoTests()
    {
        _fixture = new Fixture();
    }

    [Test]
    public void MapTo_Should_Map_Valid_Dto()
    {
        //Arrange
        var name = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();
        var product = Product.Create(name, description, stock, category);
        
        //Act
        var productDto = ProductDto.MapTo(product);
        
        //Assert
        productDto.Should().NotBeNull();
        productDto.Id.Should().Be(product.Id);
        productDto.Name.Should().Be(product.Name);
        productDto.Description.Should().Be(product.Description);
        productDto.StockQuantity.Should().Be(product.StockQuantity);
        productDto.Category.Should().NotBeNull();
        productDto.Category.Id.Should().Be(product.Category.Id);
        productDto.Category.Name.Should().Be(product.Category.Name);
    }
    
    [Test]
    public void MapTo_Should_Map_Valid_Dto_WithoutCategory()
    {
        //Arrange
        var name = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var product = Product.Create(name, description, stock, null);
        
        //Act
        var productDto = ProductDto.MapTo(product);
        
        //Assert
        productDto.Should().NotBeNull();
        productDto.Id.Should().Be(product.Id);
        productDto.Name.Should().Be(product.Name);
        productDto.Description.Should().Be(product.Description);
        productDto.StockQuantity.Should().Be(product.StockQuantity);
        productDto.Category.Should().BeNull();
    }
}