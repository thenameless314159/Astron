using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsString : Specification<PropertyInfo>
    {
        public IsString() : base(p => p.PropertyType == typeof(string))
        {
        }
    }
}
