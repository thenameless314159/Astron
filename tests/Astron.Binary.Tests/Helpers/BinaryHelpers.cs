using System;
using System.Linq;
using System.Runtime.InteropServices;
using Astron.Binary.Reader;
using Astron.Binary.Writer;

namespace Astron.Binary.Tests.Helpers
{
    public static class BinaryHelpers
    {
        public static object ReadValue(IReader reader, Type valueType)
            => typeof(IReader).GetMethod("ReadValue").MakeGenericMethod(valueType).Invoke(reader, new object[0]);

        public static object ReadValue(MappedReader reader, Type valueType)
            => typeof(MappedReader).GetMethod("ReadValue").MakeGenericMethod(valueType).Invoke(reader, new object[0]);

        public static object ReadValues(IReader reader, Type valueType, int n)
            => typeof(SubReader).GetMethod("ReadValues").MakeGenericMethod(valueType).Invoke(reader, new object[] { n });

        public static object ReadValues(MappedReader reader, Type valueType, int n)
            => typeof(MappedReader).GetMethod("ReadValues").MakeGenericMethod(valueType).Invoke(reader, new object[] { n });

        public static void WriteValue(IWriter writer, Type valueType, object value)
            => typeof(IWriter).GetMethod("WriteValue").MakeGenericMethod(valueType)
                .Invoke(writer, new object[] { value });

        public static void WriteValue(MappedWriter writer, Type valueType, object value)
            => typeof(MappedWriter).GetMethod("WriteValue").MakeGenericMethod(valueType)
                .Invoke(writer, new object[] { value });

        public static void WriteValues(IWriter writer, Type valueType, object values)
            => typeof(IWriter).GetMethods().First(m => m.GetParameters().First().ParameterType.IsArray).MakeGenericMethod(valueType)
                .Invoke(writer, new object[] { values });

        public static void WriteValues(MappedWriter writer, Type valueType, object values)
            => typeof(MappedWriter).GetMethod("WriteValues").MakeGenericMethod(valueType)
                .Invoke(writer, new object[] { values });


        public static int MarshalSizeOf(this Type t)
        {
            var marshalSizeOfMi = typeof(Marshal).GetMethods()
                .First(m => m.Name == "SizeOf" && m.IsGenericMethod && !m.GetParameters().Any())
                .MakeGenericMethod(t);

            return t != typeof(bool) ? (int)marshalSizeOfMi.Invoke(null, new object[0]) : 1;
        }
    }
}
