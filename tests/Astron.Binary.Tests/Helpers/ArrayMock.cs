using System;
using System.Linq;
using System.Reflection;

namespace Astron.Binary.Tests.Helpers
{
    public static class ArrayMock
    {
        public static object NewRandomArray(Type arrayType, int n)
            => typeof(ArrayMock).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.IsGenericMethod)
                .MakeGenericMethod(arrayType).Invoke(null, new object[] {n});

        public static T[] NewRandomArray<T>(int n)
            where T : struct
        { 
            var array = new T[n];
            for (var i = 0; i < n; i++)
                array[i] = (T)Convert.ChangeType(64, typeof(T));

            return array;
        }
    }
}
