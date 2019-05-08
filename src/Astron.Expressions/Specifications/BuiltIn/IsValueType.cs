using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsValueType : Specification<PropertyInfo>
    {
        public IsValueType() : base(p => p.PropertyType.IsValueType)
        { 
        }
    }
}
