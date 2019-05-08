using System.Reflection;
using Astron.Expressions.Helpers;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsPrimitive : Specification<PropertyInfo>
    {
        public IsPrimitive() : base(p => PrimitiveTypes.Primitives.Contains(p.PropertyType))
        {
        }
    }
}
