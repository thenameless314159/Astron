using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsStringOfString : Specification<PropertyInfo>
    {
        public IsStringOfString() : base(p => p.PropertyType.GenericTypeArguments[0].GenericTypeArguments[0] == typeof(string))
        {
        }
    }
}
