using System.Collections.Generic;
using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericCollection : Specification<PropertyInfo>
    {
        public IsGenericCollection() : base(
            p => typeof(ICollection<>).MakeGenericType(p.PropertyType.GenericTypeArguments[0]).IsAssignableFrom(p.PropertyType))
        {
        }
    }
}
