using Framework.Exceptions;
using MediatR;
using Merchandising.Application.Products.Dto;
using Merchandising.Domain.Repositories;

namespace Merchandising.Application.Products.Queries.GetById;

public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    
    public GetByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<ProductDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new NotFoundException("Product is not found");
        }
        
        return ProductDto.MapTo(product);
    }
}