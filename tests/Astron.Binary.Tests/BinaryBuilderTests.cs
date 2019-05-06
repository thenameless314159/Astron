using System;
using System.Collections.Generic;
using System.Text;
using Astron.Binary.Cache;
using Astron.Binary.Reader;
using Astron.Binary.Storage;
using Astron.Binary.Writer;
using Astron.Size;
using Xunit;

namespace Astron.Binary.Tests
{
    public class ObjectBinStorage : IBinaryStorage<object>
    {
        public Func<IReader, object> ReadValue { get; }
        public Action<IWriter, object> WriteValue { get; }
    }

    public class BinaryBuilderTests
    {
        private readonly ISizing _sizing = new SizingBuilder().Build();

        [Fact]
        public void Ctor_ShouldRegisterBuiltIn()
        {
            var builder = new BinaryBuilder(_sizing);
            Assert.True(PrimitiveBinaryCacheBuilder.IsAlreadySet);
        }

        [Fact]
        public void Ctor_ShouldNotRegisterBuiltInTwice()
        {
            var bigBuilder = new BinaryBuilder(_sizing, default, Endianness.BigEndian);
            var littleBuilder = new BinaryBuilder(_sizing);
            Assert.Equal(Endianness.BigEndian, PrimitiveBinaryCacheBuilder.Endianness);
        }

        [Fact]
        public void Register_ShouldThrow_OnTypeRegisteredTwice()
        {
            var builder = new BinaryBuilder(_sizing);
            builder.Register(new ObjectBinStorage());
            Assert.Throws<InvalidOperationException>(() => builder.Register(new ObjectBinStorage()));
        }
    }
}
