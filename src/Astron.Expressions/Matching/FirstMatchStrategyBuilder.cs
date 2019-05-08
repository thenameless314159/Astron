using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Builder;
using Astron.Expressions.Specifications;

namespace Astron.Expressions.Matching
{
    public class FirstMatchStrategyBuilder<TDel, TComp> : FactoryBuilder
        <ISpecification<PropertyInfo>, IMatchingHandler<PropertyInfo, TComp>, IMatchingStrategy<PropertyInfo, TComp>>
        where TDel : Delegate
        where TComp : IExpressionCompiler<TDel>
    {
        public override IMatchingStrategy<PropertyInfo, TComp> Build()
            => CreateFactory(dict => new SpecificationMatcher<PropertyInfo, TComp>(dict, true));
    }
}
