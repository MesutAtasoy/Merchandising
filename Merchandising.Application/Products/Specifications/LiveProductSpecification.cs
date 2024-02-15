using System.Linq.Expressions;
using Framework.Domain.Specifications;
using Merchandising.Domain.Entities;
using Merchandising.Domain.Specifications;

namespace Merchandising.Application.Products.Specifications;

public sealed class LiveProductSpecification : Specification<Product>, ILiveProductSpecification
{
    public override Expression<Func<Product, bool>> ToExpression()
    {
        return x => x.Category != null && x.StockQuantity > x.Category.MinStockQuantity && !x.IsDeleted;
    }
}