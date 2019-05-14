using System;
using System.IO;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;
using Xunit;

namespace Astron.Serialization.Tests
{
    public class SerializerBuilderTests
    {
        private readonly ISerializerBuilder _builder;

        public SerializerBuilderTests() => _builder = new SerializerBuilder();

        [Fact]
        public void Register_ShouldSetInternalCache()
        {
            _builder.Register(new SerializerStorage<MemoryStream>((s, w, v) => w.WriteBytes(v.GetBuffer())));
            Assert.NotNull(SerializeMethodCache<MemoryStream>.Serialize);
        }

        [Fact]
        public void Register_ShouldThrow_OnRegisterTwice()
        {
            _builder.Register(new SerializerStorage<string>((s, w, v) => w.WriteValue(v)));
            Assert.Throws<InvalidOperationException>(() =>
            {
                _builder.Register(new SerializerStorage<string>((s, w, v) => w.WriteValue(v)));
            });
        }

        [Fact]
        public void Build_ShouldReturnNewInstance()
        {
            var instance = _builder.Build();
            Assert.NotNull(instance);
        }
    }
}
