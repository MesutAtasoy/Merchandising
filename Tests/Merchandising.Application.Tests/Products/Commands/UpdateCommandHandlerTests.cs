using AutoFixture;
using FluentAssertions;
using Framework.Domain;
using Framework.Exceptions;
using Merchandising.Application.Products.Commands.Update;
using Merchandising.Application.Products.Commands.Update.Request;
using Merchandising.Application.Tests.Faker;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Merchandising.Application.Tests.Products.Commands;

public class UpdateCommandHandlerTests
{
    private readonly Fixture _fixture;
    private Mock<IProductRepository> _mockProductRepository;
    private Mock<ICategoryRepository> _mockCategoryRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private UpdateCommandHandler _updateCommandHandler;
    private Mock<ILogger<UpdateCommandHandler>> _mockLogger;

    public UpdateCommandHandlerTests()
    {
        _fixture = new Fixture();
        _mockProductRepository = new Mock<IProductRepository>();
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogger = new Mock<ILogger<UpdateCommandHandler>>();
        _updateCommandHandler = new UpdateCommandHandler(_mockProductRepository.Object, 
            _mockCategoryRepository.Object,
            _mockUnitOfWork.Object,
            _mockLogger.Object);
    }


    [Test]
    public async Task Update_Should_Return_ValidResponse()
    {
        //Arrange;
        var expectedProduct = ProductFaker.CreateProduct();
        var product = ProductFaker.CreateProduct();

        var updateProductDto = _fixture.Build<UpdateProductDto>()
            .With(x => x.CategoryId, expectedProduct.Category.Id)
            .Create();

        _mockProductRepository.Setup(x => x.GetByIdAsync(expectedProduct.Id))
            .ReturnsAsync(product);

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(expectedProduct.Category.Id))
            .ReturnsAsync(expectedProduct.Category);

        _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(It.IsAny<Product>());

        var updateCommand = new UpdateCommand(expectedProduct.Id, updateProductDto);

        //Act
        var result = await _updateCommandHandler.Handle(updateCommand, default);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(updateProductDto.Name);
        result.Description.Should().Be(updateProductDto.Description);
        result.StockQuantity.Should().Be(updateProductDto.StockQuantity);
        result.Category.Should().NotBeNull();
        result.Category.Id.Should().Be(expectedProduct.Category.Id);
        result.Category.Name.Should().Be(expectedProduct.Category.Name);
    }

    [Test]
    public async Task Update_Should_Return_ValidResponse_WithoutCategory()
    {
        //Arrange;
        var expectedProduct = ProductFaker.CreateProduct(true);
        var product = ProductFaker.CreateProduct(true);

        var updateProductDto = _fixture.Build<UpdateProductDto>()
            .Without(x => x.CategoryId)
            .Create();

        _mockProductRepository.Setup(x => x.GetByIdAsync(expectedProduct.Id))
            .ReturnsAsync(product);

        _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(It.IsAny<Product>());

        var updateCommand = new UpdateCommand(expectedProduct.Id, updateProductDto);

        //Act
        var result = await _updateCommandHandler.Handle(updateCommand, default);

        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(updateProductDto.Name);
        result.Description.Should().Be(updateProductDto.Description);
        result.StockQuantity.Should().Be(updateProductDto.StockQuantity);
        result.Category.Should().BeNull();
    }

    [Test]
    public async Task Update_Should_Throw_NotFoundException_When_Product_Is_NotFound()
    {
        //Arrange
        var updateProductDto = _fixture.Build<UpdateProductDto>()
            .Without(x => x.CategoryId)
            .Create();

        var unknownProductId = _fixture.Create<Guid>();

        _mockProductRepository.Setup(x => x.GetByIdAsync(unknownProductId))
            .ReturnsAsync(() => null);

        var updateCommand = new UpdateCommand(unknownProductId, updateProductDto);

        //Act
        var func = async () => await _updateCommandHandler.Handle(updateCommand, default);

        await func.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task Update_Should_Throw_NotFoundException_When_Category_Is_NotFound()
    {
        //Arrange
        var expectedProduct = ProductFaker.CreateProduct();
        var product = ProductFaker.CreateProduct();

        var updateProductDto = _fixture.Build<UpdateProductDto>()
            .With(x => x.CategoryId, expectedProduct.Category.Id)
            .Create();

        _mockProductRepository.Setup(x => x.GetByIdAsync(expectedProduct.Id))
            .ReturnsAsync(product);

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(expectedProduct.Category.Id))
            .ReturnsAsync(() => null);

        var updateCommand = new UpdateCommand(expectedProduct.Id, updateProductDto);

        //Act
        var func = async () => await _updateCommandHandler.Handle(updateCommand, default);

        //Assert
        await func.Should().ThrowAsync<NotFoundException>();
    }
}