using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Astron.Expressions.Specifications;

[assembly:InternalsVisibleTo("Astron.Expressions.Tests")]
namespace Astron.Expressions.Matching
{
    public class SpecificationMatcher<TInput, TDep> : IMatchingStrategy<TInput, TDep>
    {
        private readonly bool _breakOnFirstMatch;
        private readonly ImmutableDictionary<ISpecification<TInput>, IMatchingHandler<TInput, TDep>> _handlers;

        internal SpecificationMatcher(ImmutableDictionary<ISpecification<TInput>, IMatchingHandler<TInput, TDep>> handlers,
            bool breakOnFirstMatch = false)
        {
            _handlers = handlers;
            _breakOnFirstMatch = breakOnFirstMatch;
        }

        public void Process(TInput input, TDep dependency)
        {
            foreach (var (spec, handler) in _handlers)
            {
                if (!spec.IsSatisfiedBy(input)) continue;
                handler.OnMatch(input, dependency);
                if (_breakOnFirstMatch) break;
            }
        }
    }
}
