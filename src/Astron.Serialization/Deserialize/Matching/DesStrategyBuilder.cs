using System;
using Astron.Binary.Reader;
using Astron.Expressions.Matching;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Matching
{
    public class DesStrategyBuilder<TClass, TComp> 
        : FirstMatchStrategyBuilder<Action<IDeserializer, IReader, IMemoryPolicy, TClass>, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
    }
}
