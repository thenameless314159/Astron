using System;
using Astron.Binary.Writer;
using Astron.Expressions.Matching;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching
{
    public class SerStrategyBuilder<TClass, TComp> 
        : FirstMatchStrategyBuilder<Action<ISerializer, IWriter, TClass>, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
    }
}
