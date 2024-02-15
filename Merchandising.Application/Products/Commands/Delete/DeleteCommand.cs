using MediatR;

namespace Merchandising.Application.Products.Commands.Delete;

public class DeleteCommand : IRequest<bool>
{
    public DeleteCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}