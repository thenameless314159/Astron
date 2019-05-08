using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGeneric : Specification<PropertyInfo>
    {
        public IsGeneric() : base(p => p.PropertyType.IsGenericType)
        {
        }
    }
}
