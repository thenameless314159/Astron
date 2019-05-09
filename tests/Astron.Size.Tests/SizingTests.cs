using System;
using System.Linq;
using System.Runtime.InteropServices;
using Astron.Size.Storage;
using Astron.Size.Tests.Helpers;
using Xunit;

namespace Astron.Size.Tests
{
    public class StringSizeStorage : ISizeOfStorage<string>
    {
        public Func<ISizing, string, int> Calculate => (s, v) => v.Length + 4;
    }

    public class SizingTests
    {
        private static readonly ISizing Sizing = new SizingBuilder()
            .Register(new StringSizeStorage())
            .Build();

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
            var marshalSize = PrimitiveSizer.SizeOf(t);
            var sizeOfSize = SizeOfValue(t);

            Assert.Equal(marshalSize, sizeOfSize);
        }

        [Fact]
        public void SizeOfTWithVal_ShouldSizeString()
        {
            const string value = "four";
            Assert.Equal(8, Sizing.SizeOf(value));
        }

        private int SizeOfValue(Type t)
        {
            var sizeOfMi = typeof(ISizing).GetMethods().First(m => !m.GetParameters().Any()).MakeGenericMethod(t);

            return (int)sizeOfMi.Invoke(Sizing, new object[0]);
        }
    }
}
