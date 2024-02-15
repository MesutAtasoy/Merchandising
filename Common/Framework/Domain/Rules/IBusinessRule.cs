namespace Framework.Domain.Rules;

public interface IBusinessRule
{
    bool IsBroken();
    string Message { get; }
}