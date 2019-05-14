using System;
using Astron.Binary.Reader;
using Astron.Expressions.Matching;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Matching
{
    public interface IDesMatchingHandler<TClass, in TComp> 
        : IFirstMatchStrategyHandler<Action<IDeserializer, IReader, IMemoryPolicy, TClass>, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
    }
}
