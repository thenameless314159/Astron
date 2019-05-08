using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericString : Specification<PropertyInfo>
    {
        public IsGenericString() : base(p => p.PropertyType.GenericTypeArguments[0] == typeof(string))
        {
        }
    }
}
