using FluentAssertions;
using Framework.Domain;
using Framework.Exceptions;
using Merchandising.Application.Products.Commands.Delete;
using Merchandising.Application.Tests.Faker;
using Merchandising.Domain.Repositories;
using Moq;
using Microsoft.Extensions.Logging;

namespace Merchandising.Application.Tests.Products.Commands;

public class DeleteCommandHandlerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private DeleteCommandHandler _deleteCommandHandler;
    private Mock<ILogger<DeleteCommandHandler>> _mockLogger;

    public DeleteCommandHandlerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogger = new Mock<ILogger<DeleteCommandHandler>>();

        _deleteCommandHandler = new DeleteCommandHandler(_mockProductRepository.Object,
            _mockUnitOfWork.Object,
            _mockLogger.Object);
    }


    [Test]
    public async Task Delete_Should_Return_ValidResponse()
    {
        //Arrange 
        var product = ProductFaker.CreateProduct();

        _mockProductRepository.Setup(x => x.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        var deleteCommand = new DeleteCommand(product.Id);
        
        //Act
        var result = await _deleteCommandHandler.Handle(deleteCommand, default);

        //Assert
        result.Should().BeTrue();
    }
    
    [Test]
    public async Task Delete_Should_Throw_NotFoundException_When_Product_Is_Null()
    {
        //Arrange 
        var product = ProductFaker.CreateProduct();

        _mockProductRepository.Setup(x => x.GetByIdAsync(product.Id))
            .ReturnsAsync(()=> null);

        var deleteCommand = new DeleteCommand(product.Id);
        
        //Act
        var result = async () =>  await _deleteCommandHandler.Handle(deleteCommand, default);

        //Assert
        await result.Should().ThrowAsync<NotFoundException>();
    }
}