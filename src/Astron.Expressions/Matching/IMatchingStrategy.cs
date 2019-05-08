using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Expressions.Matching
{
    public interface IMatchingStrategy<in TInput, in TDependency>
    {
        void Process(TInput input, TDependency dependency);
    }
}
