﻿using Framework.Domain;
using Framework.Exceptions;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Merchandising.Application.Products.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateCommandHandler> _logger;

    public CreateCommandHandler(IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork, 
        ILogger<CreateCommandHandler> logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProductDto> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        Category? category = null;

        if (request.CategoryId.HasValue)
        {
            category = await _categoryRepository.GetByIdAsync(request.CategoryId.Value);
            if (category == null)
            {
                throw new NotFoundException("Category is not found");
            }
        }

        var product = Product.Create(request.Name, request.Description, request.StockQuantity, category);

        await _productRepository.AddAsync(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        _logger.LogInformation($"Product is added. {product}");
        
        return ProductDto.MapTo(product);
    }
}