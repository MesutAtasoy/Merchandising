using AutoFixture;
using Merchandising.Domain.Entities;

namespace Merchandising.Application.Tests.Faker;

public static class ProductFaker
{
    public static Product CreateProduct(bool withoutCategory = false)
    {
        var fixture = new Fixture();
        var name = fixture.Create<string>();
        var description = fixture.Create<string>();
        var stock = fixture.Create<int>();
        var category = fixture.Create<Category>();
        var product = Product.Create(name, description, stock, category);

        return product;
    }
}