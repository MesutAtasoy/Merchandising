using Framework.Domain.Exceptions;
using Framework.Domain.Rules;

namespace Framework.Domain;

public abstract class Entity
{
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}