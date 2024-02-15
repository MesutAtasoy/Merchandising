using MediatR;
using Merchandising.Application.Products.Commands.Update.Request;
using Merchandising.Application.Products.Dto;

namespace Merchandising.Application.Products.Commands.Update;

public class UpdateCommand : IRequest<ProductDto>
{
    public UpdateCommand(Guid id, UpdateProductDto request)
    {
        Id = id;
        Request = request;
    }

    public Guid Id { get; }
    public UpdateProductDto Request { get; }
}