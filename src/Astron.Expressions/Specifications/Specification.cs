using System;
using System.Linq.Expressions;

namespace Astron.Expressions.Specifications
{
    public class Specification<T> : BaseSpecification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public override Expression<Func<T, bool>> ToExpression()
            => _predicate;

        public Specification(Expression<Func<T, bool>> predicate)
            => _predicate = predicate;
    }
}
