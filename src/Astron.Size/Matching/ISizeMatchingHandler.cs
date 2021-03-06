﻿using System;
using Astron.Expressions.Matching;
using Astron.Size.Expressions;

namespace Astron.Size.Matching
{
    public interface ISizeMatchingHandler<TClass, in TComp> 
        : IFirstMatchStrategyHandler<Func<ISizing, TClass, int>, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
    }
}
