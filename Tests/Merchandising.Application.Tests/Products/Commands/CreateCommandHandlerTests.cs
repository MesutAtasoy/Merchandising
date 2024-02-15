using AutoFixture;
using FluentAssertions;
using Framework.Domain;
using Framework.Domain.Exceptions;
using Framework.Exceptions;
using Merchandising.Application.Products.Commands.Create;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Merchandising.Application.Tests.Products.Commands;

public class CreateCommandHandlerTests
{
    private readonly Fixture _fixture;
    private Mock<IProductRepository> _mockProductRepository;
    private Mock<ICategoryRepository> _mockCategoryRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<ILogger<CreateCommandHandler>> _mockLogger;
    private CreateCommandHandler _createCommandHandler;

    public CreateCommandHandlerTests()
    {
        _fixture = new Fixture();
        _mockProductRepository = new Mock<IProductRepository>();
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogger = new Mock<ILogger<CreateCommandHandler>>();
        _createCommandHandler = new CreateCommandHandler(_mockProductRepository.Object,
            _mockCategoryRepository.Object,
            _mockUnitOfWork.Object,
            _mockLogger.Object);
    }


    [Test]
    public async Task Create_Should_Return_ValidResponse()
    {
        //Arrange
        var category = _fixture.Create<Category>();

        var createCommand = _fixture.Build<CreateCommand>()
            .With(x => x.CategoryId, category.Id)
            .Create();

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(category.Id))
            .ReturnsAsync(category);

        _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(It.IsAny<Product>());

        //Act
        var result = await _createCommandHandler.Handle(createCommand, default);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(createCommand.Name);
        result.Description.Should().Be(createCommand.Description);
        result.StockQuantity.Should().Be(createCommand.StockQuantity);
        result.Category.Should().NotBeNull();
        result.Category.Id.Should().Be(category.Id);
        result.Category.Name.Should().Be(category.Name);
    }

    [Test]
    public async Task Create_Should_Return_ValidResponse_WithoutCategory()
    {
        //Arrange
        var createCommand = _fixture.Build<CreateCommand>()
            .Without(x => x.CategoryId)
            .Create();

        _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(It.IsAny<Product>());

        //Act
        var result = await _createCommandHandler.Handle(createCommand, default);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();
        result.Name.Should().Be(createCommand.Name);
        result.Description.Should().Be(createCommand.Description);
        result.StockQuantity.Should().Be(createCommand.StockQuantity);
        result.Category.Should().BeNull();
    }

    [Test]
    public async Task Create_Should_Throw_NotFoundException_When_Category_Is_NotFound()
    {
        //Arrange
        var createCommand = _fixture.Build<CreateCommand>()
            .Create();

        _mockCategoryRepository.Setup(x => x.GetByIdAsync(createCommand.CategoryId.Value))
            .ReturnsAsync(() => null);

        //Act
        var func = async () => await _createCommandHandler.Handle(createCommand, default);

        await func.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task Create_Should_Throw_BusinessRuleException_When_Name_is_Empty()
    {
        //Arrange
        var createCommand = _fixture.Build<CreateCommand>()
            .Without(x => x.Name)
            .Without(x => x.CategoryId)
            .Create();

        _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync(It.IsAny<Product>());

        //Act
        var func = async () => await _createCommandHandler.Handle(createCommand, default);

        //Assert
        await func.Should().ThrowAsync<BusinessRuleValidationException>();
    }
}