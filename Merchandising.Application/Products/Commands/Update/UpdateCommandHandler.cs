using Framework.Domain;
using Framework.Exceptions;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;

namespace Merchandising.Application.Products.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCommandHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
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
        
        return ProductDto.MapTo(product);
    }
}