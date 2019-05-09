using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Astron.Size.Cache;
using Astron.Size.Storage;
using Astron.Size.Tests.Helpers;
using Xunit;

namespace Astron.Size.Tests
{
    public class SizingBuilderTests
    {
        private ISizingBuilder _builder;

        public SizingBuilderTests()
        {
            _builder = new SizingBuilder();
        }

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
        public void Ctor_ShouldRegisterValueStorageOf(Type type)
        {
            var sizeOf = PrimitiveSizer.SizeOf(type);
            Assert.Equal(sizeOf, GetSizeValueCacheOf(type));
        }

        [Fact]
        public void Register_ValueStorage_ShouldSetCache()
        {
            _builder.Register(new SizeValueStorage<decimal>(16));
            Assert.Equal(16, SizeValueCache<decimal>.Value);
        }

        [Fact]
        public void Register_ValueStorage_ShouldNotSetAlreadyRegistered()
        {
            _builder.Register(new SizeValueStorage<decimal>(16));
            _builder.Register(new SizeValueStorage<decimal>(32));
            Assert.Equal(16, SizeValueCache<decimal>.Value);
        }

        [Fact]
        public void Register_SizeOfStorage_ShouldSetCache()
        {
            _builder.Register(new SizeOfStorage<StringBuilder>((s, sb) => sb.Length));
            Assert.NotNull(SizeOfCache<StringBuilder>.Calculate);
        }

        [Fact]
        public void Register_SizeOfStorage_ShouldThrow_OnAlreadyRegistered()
        {
            _builder.Register(new SizeOfStorage<MemoryStream>((s, ms) => (int)ms.Length));
            Assert.Throws<InvalidOperationException>(
                () => _builder.Register(new SizeOfStorage<MemoryStream>((s, ms) => ms.Capacity)));
        }

        public static int GetSizeValueCacheOf(Type type)
            => (int)typeof(SizeValueCache<>).MakeGenericType(type).GetProperty("Value").GetValue(null);
    }
}
