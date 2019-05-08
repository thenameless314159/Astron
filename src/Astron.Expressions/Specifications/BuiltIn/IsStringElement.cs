using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsStringElement : Specification<PropertyInfo>
    {
        public IsStringElement() : base(p => p.PropertyType.GetElementType() == typeof(string))
        {
        }
    }
}
