using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Astron.Expressions.Helpers
{
    public static class PropertyHelper
    {
        public static ImmutableArray<PropertyInfo> SortPropertiesOf<TClass>()
        {
            var builder = ImmutableArray.CreateBuilder<PropertyInfo>();
            var types = new Stack<TypeInfo>(24);

            var currentType = typeof(TClass).GetTypeInfo();
            do
            {
                types.Push(currentType);
                currentType = currentType.BaseType.GetTypeInfo();
            } while (currentType.BaseType != null);

            foreach (var type in types)
                builder.AddRange(
                    type.GetProperties().Where(p => p.CanRead
                                                    && p.CanWrite
                                                    && builder.All(pi => pi.Name != p.Name)));

            return builder.ToImmutableArray();
        }
    }
}
