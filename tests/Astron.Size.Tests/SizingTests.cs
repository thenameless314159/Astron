using System;
using System.Linq;
using System.Runtime.InteropServices;
using Astron.Size.BuiltIn;
using Xunit;

namespace Astron.Size.Tests
{
    public class SizingTests
    {
        private static readonly ISizing Sizing = new SizingBuilder().Register(new StringSizeStorage()).Build();

        [Theory]
        [InlineData(typeof(bool))]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(short))]
        [InlineData(typeof(ushort))]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        public void SizeOfT_ShouldSizePrimitives(Type t)
        {
            var marshalSize = MarshalSizeOf(t);
            var sizeOfSize = SizeOfValue(t);

            Assert.Equal(marshalSize, sizeOfSize);
        }

        [Fact]
        public void SizeOfTWithVal_ShouldSizeString()
        {
            var str = "four";
            Assert.Equal(8, Sizing.SizeOf(str));
        }

        private int SizeOfValue(Type t)
        {
            var sizeOfMi = typeof(ISizing).GetMethods().First(m => !m.GetParameters().Any()).MakeGenericMethod(t);

            return (int)sizeOfMi.Invoke(Sizing, new object[0]);
        }

        // marshal size of methods are obsolete therefore bool value use old CLR value (that is 4)
        private int MarshalSizeOf(Type t)
        {
            var marshalSizeOfMi = typeof(Marshal).GetMethods()
                .First(m => m.Name == "SizeOf" && m.IsGenericMethod && !m.GetParameters().Any())
                .MakeGenericMethod(t);

            return t != typeof(bool) ? (int)marshalSizeOfMi.Invoke(null, new object[0]) : 1;
        }
    }
}
