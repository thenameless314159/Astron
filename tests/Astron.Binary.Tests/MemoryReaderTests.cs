using System;
using System.Collections.Generic;
using System.IO;
using Astron.Binary.Cache;
using Astron.Binary.Reader;
using Astron.Binary.Tests.Helpers;
using Xunit;
using static Astron.Binary.Tests.Helpers.BinaryHelpers;

namespace Astron.Binary.Tests
{
    public class SubReader : MemoryReader
    {
        internal SubReader(ReadOnlyMemory<byte> buffer) : base(buffer)
        {
        }
    }

    public class MemoryReaderTests
    {
        private readonly SubReader _binReader;
        private readonly MappedReader _mapReader;

        static MemoryReaderTests() => PrimitiveBinaryCacheBuilder.RegisterAllBigEndian();

        public MemoryReaderTests()
        {
            var buffer = BufferMock.Buffer;
            _mapReader = new MappedReader(new MemoryStream(buffer));
            _binReader = new SubReader(new ReadOnlyMemory<byte>(buffer));
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
        public void ReadValue_ShouldReturnCorrectValue(Type t)
        {
            var binValue = ReadValue(_binReader, t);
            var mapValue = ReadValue(_mapReader, t);

            Assert.Equal(mapValue, binValue);
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
        public void ReadValue_ShouldAdvancePosition(Type t)
        {
            ReadValue(_binReader, t);
            Assert.Equal(t.MarshalSizeOf(), _binReader.Position);
        }

        [Theory]
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
        public void ReadValues_ShouldReturnCorrectValues(Type t)
        {
            const int count = 16;
            var binValue = ReadValues(_binReader, t, count);
            var mapValue = ReadValues(_mapReader, t, count);

            Assert.Equal(mapValue, binValue);
        }

        [Theory]
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
        public void ReadValues_ShouldAdvancePosition(Type t)
        {
            const int count = 16;
            ReadValues(_binReader, t, count);
            Assert.Equal(t.MarshalSizeOf() * count, _binReader.Position);
        }

        [Fact]
        public void ReadValue_ShouldThrow_OnNotRegistered()
            => Assert.Throws<KeyNotFoundException>(() => _binReader.ReadValue<string>());

        [Fact]
        public void ReadValues_ShouldThrow_OnNotRegistered()
            => Assert.Throws<KeyNotFoundException>(() => _binReader.ReadValues<string>(1));
    }
}
