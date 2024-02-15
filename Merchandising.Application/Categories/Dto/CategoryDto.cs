using Merchandising.Domain.Entities;

namespace Merchandising.Application.Categories.Dto;

public record CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public static CategoryDto? MapTo(Category? category)
    {
        return category != null ? new CategoryDto { Id = category.Id, Name = category.Name } : null;
    }
}