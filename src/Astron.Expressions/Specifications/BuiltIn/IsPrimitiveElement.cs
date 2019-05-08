using System.Reflection;
using Astron.Expressions.Helpers;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsPrimitiveElement : Specification<PropertyInfo>
    {
        public IsPrimitiveElement() : base(p => PrimitiveTypes.Primitives.Contains(p.PropertyType.GetElementType()))
        {
        }
    }
}
