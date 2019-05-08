using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericValueType : Specification<PropertyInfo>
    {
        public IsGenericValueType() : base(p => p.PropertyType.GenericTypeArguments[0].IsValueType)
        {
        }
    }
}
