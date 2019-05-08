using System;
using System.Linq.Expressions;

namespace Astron.Expressions.Specifications
{
    public abstract class BaseSpecification<TInput> : ISpecification<TInput>
    {
        private Func<TInput, bool> _compiledPredicate;
        public abstract Expression<Func<TInput, bool>> ToExpression(); // expression to support IQueryable

        public bool IsSatisfiedBy(TInput input)
            => (_compiledPredicate ?? (_compiledPredicate = ToExpression().Compile()))(input);

        public AndSpecification<TInput> And(BaseSpecification<TInput> specification) 
            => new AndSpecification<TInput>(this, specification);

        public OrSpecification<TInput> Or(BaseSpecification<TInput> specification) 
            => new OrSpecification<TInput>(this, specification);
    }
}
