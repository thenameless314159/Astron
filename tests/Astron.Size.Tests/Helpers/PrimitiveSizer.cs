using System;
using System.Linq;

namespace Astron.Size.Tests.Helpers
{
    public static class PrimitiveSizer
    {
        public static unsafe int SizeOf<T>() where T : unmanaged
            => sizeof(T);

        public static int SizeOf(Type type)
            => (int)typeof(PrimitiveSizer).GetMethods().First(m => m.IsGenericMethod).MakeGenericMethod(type)
                .Invoke(null, new object[0]);
    }
}
