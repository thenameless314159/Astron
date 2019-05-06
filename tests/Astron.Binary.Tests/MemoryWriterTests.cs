using Astron.Binary.Tests.Helpers;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Astron.Binary.Cache;
using Astron.Binary.Writer;
using Astron.Size;
using Astron.Size.Storage;
using Xunit;
using static Astron.Binary.Tests.Helpers.BinaryHelpers;

namespace Astron.Binary.Tests
{
    public class SimpleOwner : IMemoryOwner<byte>
    {
        public void Dispose()
        {
        }

        public SimpleOwner(Memory<byte> memory) => Memory = memory;

        public Memory<byte> Memory { get; }
    }

    public class StringSizeStorage : ISizeOfStorage<string>
    {
        public Func<ISizing, string, int> Calculate => (s, v) => 4 + v.Length;
    }

    public class MemoryWriterTests
    {
        private readonly IWriter _binWriter;
        private readonly MappedWriter _mapWriter;

        static MemoryWriterTests() => PrimitiveBinaryCacheBuilder.RegisterAllBigEndian();

        public MemoryWriterTests()
        {
            _mapWriter = new MappedWriter(new MemoryStream());
            _binWriter = new MemoryWriter(new SimpleOwner(new Memory<byte>(new byte[64])), SizingProvider.Sizing, 64);
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
        public void WriteValue_ShouldWriteCorrectValue(Type t)
        {
            const int value = 64;

            WriteValue(_binWriter, t, Convert.ChangeType(value, t));
            WriteValue(_mapWriter, t, Convert.ChangeType(value, t));

            Assert.Equal(_mapWriter.GetData(), _binWriter.GetBuffer().ToArray());
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
        public void WriteValue_ShouldAdvancePosition(Type t)
        {
            const int value = 64;

            WriteValue(_binWriter, t, Convert.ChangeType(value, t));
            Assert.Equal(t.MarshalSizeOf(), _binWriter.Position);
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
        public void WriteValues_ShouldWriteCorrectValues(Type t)
        {
            var values = ArrayMock.NewRandomArray(t, 8);

            WriteValues(_binWriter, t, values);
            WriteValues(_mapWriter, t, values);

            Assert.Equal(_mapWriter.GetData(), _binWriter.GetBuffer().ToArray());
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
        public void WriteValues_ShouldAdvancePosition(Type t)
        {
            var values = ArrayMock.NewRandomArray(t, 8);
            WriteValues(_binWriter, t, values);

            Assert.Equal(t.MarshalSizeOf() * 8, _binWriter.Position);
        }

        [Fact]
        public void WriteValue_ShouldThrow_OnNotRegistered()
            => Assert.Throws<KeyNotFoundException>(() => _binWriter.WriteValue(string.Empty));

        [Fact]
        public void WriteValues_ShouldThrow_OnNotRegistered()
            => Assert.Throws<KeyNotFoundException>(() => _binWriter.WriteValues(new string[1]));
    }
}
