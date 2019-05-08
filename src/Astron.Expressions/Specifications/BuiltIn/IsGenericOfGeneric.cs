using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericOfGeneric : Specification<PropertyInfo>
    {
        public IsGenericOfGeneric() : base(p => p.PropertyType.GenericTypeArguments[0].IsGenericType)
        {
        }
    }
}
