using System;
using System.Reflection;

namespace Astron.Expressions.Matching
{
    public interface IFirstMatchStrategyHandler<TDel, in TComp> : IMatchingHandler<PropertyInfo, TComp>
        where TDel : Delegate
        where TComp : IExpressionCompiler<TDel>
    {
    }
}
