using System.Reflection;
using Astron.Expressions.Builder;
using Astron.Expressions.Matching;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching
{
    public interface ISerMatchingProviderOf<TClass, in TComp> : IBuilder<IMatchingStrategy<PropertyInfo, TComp>>
        where TComp : ISerExprCompilerOf<TClass>
    {
    }
}
