using System.Collections.Generic;
using System.Reflection;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsCollectionOfCollection : Specification<PropertyInfo>
    {
        public IsCollectionOfCollection() : base(
            p => typeof(ICollection<>).MakeGenericType(p.PropertyType.GenericTypeArguments[0].GenericTypeArguments[0])
                .IsAssignableFrom(p.PropertyType.GenericTypeArguments[0]))
        {
        }
    }
}
