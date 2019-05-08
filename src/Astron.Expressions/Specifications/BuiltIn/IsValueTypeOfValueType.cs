using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsValueTypeOfValueType : Specification<PropertyInfo>
    {
        public IsValueTypeOfValueType() : base(p => p.PropertyType.GenericTypeArguments[0].GenericTypeArguments[0].IsValueType)
        {
        }
    }
}
