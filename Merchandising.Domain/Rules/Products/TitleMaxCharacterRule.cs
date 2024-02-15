using Framework.Domain.Rules;

namespace Merchandising.Domain.Rules.Products;

public class TitleMaxCharacterRule : IBusinessRule
{
    private readonly string _title;
    
    public TitleMaxCharacterRule(string Name)
    {
        _title = Name;
    }
    
    /// <summary>
    /// Checks Name's length  
    /// </summary>
    /// <returns></returns>
    public bool IsBroken()
    {
        return _title.Length > 200;
    }
    
    public string Message => "Name must be at most 200 characters";
}