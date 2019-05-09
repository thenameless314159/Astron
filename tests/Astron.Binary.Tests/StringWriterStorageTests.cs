using Astron.Binary.Tests.Helpers;
using System;
using System.IO;
using Astron.Binary.Cache;
using Astron.Binary.Storage;
using Astron.Binary.Writer;
using Xunit;

namespace Astron.Binary.Tests
{
    public class StringWriterStorageTests
    {
        private readonly IWriter _binWriter;
        private readonly MappedWriter _mapWriter;

        private const string Value = "shouldSize12";

        static StringWriterStorageTests() => PrimitiveBinaryCacheBuilder.RegisterAllBigEndian();

        public StringWriterStorageTests()
        {
            _mapWriter = new MappedWriter(new MemoryStream());
            _binWriter = new MemoryWriter(new SimpleOwner(new Memory<byte>(new byte[16])), SizingProvider.Sizing, 64);
        }

        [Fact]
        public void WriteValue_Utf8_ShouldWriteCorrectString()
        {

            var storage = new Utf8BinaryStorage();
            _mapWriter.WriteUtf8(Value);
            storage.WriteValue(_binWriter, Value);

            Assert.Equal(_mapWriter.GetData(), _binWriter.GetBuffer().ToArray());
        }

        [Fact]
        public void WriteValue_Ascii_ShouldWriteCorrectString()
        {

            var storage = new AsciiBinaryStorage();
            _mapWriter.WriteAscii(Value);
            storage.WriteValue(_binWriter, Value);

            Assert.Equal(_mapWriter.GetData(), _binWriter.GetBuffer().ToArray());
        }
    }
}
