using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Builder;
using Astron.Expressions.Matching;
using Astron.Size.Expressions;

namespace Astron.Size.Matching
{
    public interface ISizeMatchingProviderOf<TClass, in TComp> : IBuilder<IMatchingStrategy<PropertyInfo, TComp>>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
    }
}
