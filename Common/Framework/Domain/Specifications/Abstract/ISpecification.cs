using System.Linq.Expressions;

namespace Framework.Domain.Specifications.Abstract;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
    bool IsSatisfiedBy(T obj);
}