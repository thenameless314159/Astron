using System;
using System.Reflection;
using Astron.Binary.Writer;
using Astron.Expressions.Matching;

namespace Astron.Serialization.Serialize.Expressions
{
    public interface ISerMethodBuilderOf<in TClass, out TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        IMatchingStrategy<PropertyInfo, TComp> Strategy { set; }

        Action<ISerializer, IWriter, TClass> Build();
    }
}
