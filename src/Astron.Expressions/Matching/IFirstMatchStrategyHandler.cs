using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Specifications;

namespace Astron.Expressions.Matching
{
    public interface IFirstMatchStrategyHandler<TDel, in TComp> : IMatchingHandler<PropertyInfo, TComp>
        where TDel : Delegate
        where TComp : IExpressionCompiler<TDel>
    {
    }
}
