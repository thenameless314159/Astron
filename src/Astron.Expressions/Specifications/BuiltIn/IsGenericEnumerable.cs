using System.Collections.Generic;
using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericEnumerable : Specification<PropertyInfo>
    {
        public IsGenericEnumerable() : base(p => typeof(IEnumerable<>).IsAssignableFrom(p.PropertyType))
        {
        }
    }
}
