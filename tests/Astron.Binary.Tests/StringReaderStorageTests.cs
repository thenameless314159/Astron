using System;
using System.Buffers.Binary;
using System.IO;
using Astron.Binary.Cache;
using Astron.Binary.Reader;
using Astron.Binary.Storage;
using Astron.Binary.Tests.Helpers;
using Xunit;

namespace Astron.Binary.Tests
{
    public class StringReaderStorageTests
    {
        private readonly IReader _binReader;
        private readonly MappedReader _mapReader;

        static StringReaderStorageTests() => PrimitiveBinaryCacheBuilder.RegisterAllBigEndian();

        public StringReaderStorageTests()
        {
            var mock = ArrayMock.NewRandomArray<byte>(28);
            var buffer = new byte[32];
            var memory = new Memory<byte>(buffer);
            BinaryPrimitives.WriteInt32BigEndian(memory.Span, 28);
            mock.CopyTo(memory.Slice(4));

            _mapReader = new MappedReader(new MemoryStream(buffer));
            _binReader = new MemoryReader(memory);
        }

        [Fact]
        public void ReadValue_Utf8_ShouldReturnCorrectString()
        {
            var storage = new Utf8BinaryStorage();
            var binStr = storage.ReadValue(_binReader);
            var mapStr = _mapReader.ReadAscii(); 

            Assert.Equal(mapStr, binStr);
        }

        [Fact]
        public void ReadValue_Ascii_ShouldReturnCorrectString()
        {
            var storage = new AsciiBinaryStorage();
            var binStr = storage.ReadValue(_binReader);
            var mapStr = _mapReader.ReadAscii();

            Assert.Equal(mapStr, binStr);
        }
    }
}
