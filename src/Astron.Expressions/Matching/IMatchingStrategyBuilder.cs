using System;
using System.Collections.Generic;
using System.Text;
using Astron.Expressions.Builder;

namespace Astron.Expressions.Matching
{
    public interface IMatchingStrategyBuilder<in TInput, in TDep, out TFact> 
        : IFactoryBuilder<TInput, TDep, TFact> where TFact : IMatchingStrategy<TInput, TDep>
    {
    }
}
