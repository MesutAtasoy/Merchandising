using MediatR;
using Merchandising.Application.Products.Dto;

namespace Merchandising.Application.Products.Queries.GetById;

public class GetByIdQuery : IRequest<ProductDto>
{
    public GetByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}