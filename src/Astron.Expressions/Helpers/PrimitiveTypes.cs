using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Astron.Expressions.Helpers
{
    public static class PrimitiveTypes
    {
        public static ImmutableList<Type> Primitives = ImmutableList
            .Create(typeof(byte), typeof(sbyte), typeof(bool), typeof(short), typeof(ushort),
                typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(float), typeof(double),
                typeof(decimal));
    }
}
