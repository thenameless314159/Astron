using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions;
using Astron.Expressions.Matching;

namespace Astron.Size.Expressions
{
    public interface ICalculateFuncBuilderOf<in TClass, out TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        IMatchingStrategy<PropertyInfo, TComp> Strategy { set; }

        Func<ISizing, TClass, int> Build();
    }
}
