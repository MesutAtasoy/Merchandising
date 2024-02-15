using Framework.Domain;
using Framework.Exceptions;
using MediatR;
using Merchandising.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Merchandising.Application.Products.Commands.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCommandHandler> _logger;

    public DeleteCommandHandler(IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteCommandHandler> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new NotFoundException("Product is not found");
        }
        
        product.Delete();

        await _productRepository.UpdateAsync(product);

        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation($"Product is deleted. {product}");

        return true;
    }
}