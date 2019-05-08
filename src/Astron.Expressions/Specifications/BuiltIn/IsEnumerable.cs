using System.Collections;
using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsEnumerable : Specification<PropertyInfo>
    {
        public IsEnumerable() : base(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType))
        {
        }
    }
}
