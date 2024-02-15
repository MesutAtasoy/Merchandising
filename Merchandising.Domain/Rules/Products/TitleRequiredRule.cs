using Framework.Domain.Rules;

namespace Merchandising.Domain.Rules.Products;

public class TitleRequiredRule : IBusinessRule
{
    private readonly string _title;

    public TitleRequiredRule(string Name)
    {
        _title = Name;
    }

    /// <summary>
    /// It checks whether Name is null or empty string 
    /// </summary>
    /// <returns>Returns true if Name is null or empty string.</returns>
    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(_title);
    }

    public string Message => "Name is required";
}