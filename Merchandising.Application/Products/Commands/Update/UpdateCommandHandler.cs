using Framework.Domain;
using Framework.Exceptions;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Merchandising.Application.Products.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCommandHandler> _logger;

    public UpdateCommandHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, 
        ILogger<UpdateCommandHandler> logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException("Product is not found");
        }

        Category? category = null;
        if (request.Request.CategoryId.HasValue)
        {
            category = await _categoryRepository.GetByIdAsync(request.Request.CategoryId.Value);

            if (category == null)
            {
                throw new NotFoundException("Category is not found");
            }
        }

        product.Update(request.Request.Name, request.Request.Description, request.Request.StockQuantity, category);

        await _productRepository.UpdateAsync(product);

        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation($"Product is updated. {product}");

        return ProductDto.MapTo(product);
    }
}