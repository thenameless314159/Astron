using System;
using Astron.Expressions.Matching;
using Astron.Size.Expressions;

namespace Astron.Size.Matching
{
    public class SizeStrategyBuilder<TClass, TComp> 
        : FirstMatchStrategyBuilder<Func<ISizing, TClass, int>, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
    }
}
