using AutoFixture;
using FluentAssertions;
using Framework.Domain.Exceptions;
using Merchandising.Domain.Entities;

namespace Merchandising.Domain.Tests.Entities;

public class ProductTests
{
    private readonly Fixture _fixture;

    public ProductTests()
    {
        _fixture = new Fixture();
    }

    [Test]
    public void Create_Should_Return_Product()
    {
        //Arrange
        var name = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();

        //Act
        var product = Product.Create(name, description, stock, category);

        //Assert
        product.Should().NotBeNull();
        product.Id.Should().NotBeEmpty();
        product.Name.Should().Be(name);
        product.Description.Should().Be(description);
        product.StockQuantity.Should().Be(stock);
        product.Category.Should().BeEquivalentTo(category);
        product.IsDeleted.Should().BeFalse();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Create_Should_Throw_BusinessRuleException_When_name_is_empty(string name)
    {
        //Arrange
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => Product.Create(name, description, stock, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }
    
    [TestCase(250)]
    [TestCase(201)]
    public void Create_Should_Throw_BusinessRuleException_When_character_of_name_is_greater_than_limit(int limit)
    {
        //Arrange
        var name = string.Join("", Enumerable.Repeat(0, limit).Select(n => (char)new Random().Next(127))); ;
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => Product.Create(name, description, stock, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }
    
    [Test]
    public void Create_Should_Throw_BusinessRuleException_When_Stock_is_less_than_0()
    {
        //Arrange
        var description = _fixture.Create<string>();
        var name = _fixture.Create<string>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => Product.Create(name, description, -1, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }

    [Test]
    public void Update_Should_Return_Updated_Value()
    {
        //Arrange
        var product = _fixture.Create<Product>();
        var category = _fixture.Create<Category>();
        var name = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();

        //Act
        var updatedProduct = product.Update(name, description, stock, category);
        
        //Assert
        updatedProduct.Should().NotBeNull();
        updatedProduct.Id.Should().NotBeEmpty();
        updatedProduct.Name.Should().Be(name);
        updatedProduct.Description.Should().Be(description);
        updatedProduct.StockQuantity.Should().Be(stock);
        updatedProduct.Category.Should().BeEquivalentTo(category);
        updatedProduct.IsDeleted.Should().BeFalse();
    }
    
    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void Update_Should_Throw_BusinessRuleException_When_name_is_empty(string name)
    {
        //Arrange
        var product = _fixture.Create<Product>();
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => product.Update(name, description, stock, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }
    
    [TestCase(250)]
    [TestCase(201)]
    public void Update_Should_Throw_BusinessRuleException_When_character_of_name_is_greater_than_limit(int limit)
    {
        //Arrange
        var product = _fixture.Create<Product>();
        var name = string.Join("", Enumerable.Repeat(0, limit).Select(n => (char)new Random().Next(127))); ;
        var description = _fixture.Create<string>();
        var stock = _fixture.Create<int>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => product.Update(name, description, stock, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }
    
    [Test]
    public void Update_Should_Throw_BusinessRuleException_When_Stock_is_less_than_0()
    {
        //Arrange
        var product = _fixture.Create<Product>();
        var name = _fixture.Create<string>();
        var description = _fixture.Create<string>();
        var category = _fixture.Create<Category>();

        //Act
        var func = () => product.Update(name, description, -1, category);

        //Assert
        func.Should().Throw<BusinessRuleValidationException>();
    }

    [Test]
    public void Delete_Should_Return_Valid_Value()
    {
        //Arrange
        var product = _fixture.Create<Product>();
        
        //Act
        product.Delete();
        
        //Assert
        product.IsDeleted.Should().BeTrue();
    }
}