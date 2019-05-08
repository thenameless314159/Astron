using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsArray : Specification<PropertyInfo>
    {
        public IsArray() : base(p => p.PropertyType.IsArray)
        {
        }
    }
}
