using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsValueTypeElement : Specification<PropertyInfo>
    {
        public IsValueTypeElement() : base(p => p.PropertyType.GetElementType().IsValueType)
        {
        }
    }
}
