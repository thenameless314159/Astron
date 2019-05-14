using System;
using System.Reflection;
using Astron.Binary.Reader;
using Astron.Expressions.Matching;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Expressions
{
    public interface IDesMethodBuilderOf<in TClass, out TComp>
        where TComp : IDesExprCompilerOf<TClass>
    {
        IMatchingStrategy<PropertyInfo, TComp> Strategy { set; }

        Action<IDeserializer, IReader, IMemoryPolicy, TClass> Build();
    }
}
