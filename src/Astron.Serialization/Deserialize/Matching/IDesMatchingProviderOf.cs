using System.Reflection;
using Astron.Expressions.Builder;
using Astron.Expressions.Matching;

namespace Astron.Serialization.Deserialize.Matching
{
    public interface IDesMatchingProviderOf<TClass, in TComp> : IBuilder<IMatchingStrategy<PropertyInfo, TComp>> 
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
    }
}
