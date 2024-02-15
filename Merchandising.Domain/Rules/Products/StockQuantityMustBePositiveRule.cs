using Framework.Domain.Rules;

namespace Merchandising.Domain.Rules.Products;

public class StockQuantityMustBePositiveRule : IBusinessRule
{
    private readonly int _quantity;

    public StockQuantityMustBePositiveRule(int quantity)
    {
        _quantity = quantity;
    }

    public bool IsBroken()
    {
        return _quantity < 0;
    }

    public string Message => "Stock count must be positive number";
}