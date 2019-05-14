using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Astron.Memory;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;
using Xunit;

namespace Astron.Serialization.Tests
{
    public class DeserializerBuilderTests
    {
        class Deserializable { public int Value { get; set; } }
        class SecondDeserializable { public int Value { get; set; } }

        private readonly IDeserializerBuilder _builder;

        public DeserializerBuilderTests() => _builder = new DeserializerBuilder(new HeapAllocationPolicy());

        [Fact]
        public void Register_ShouldSetInternalCache()
        {
            _builder.Register(new DeserializerStorage<Deserializable>((d, r, p, v) => v.Value = r.ReadValue<int>()));
            Assert.NotNull(DeserializeMethodCache<Deserializable>.Deserialize);
        }

        [Fact]
        public void Register_ShouldThrow_OnRegisterTwice()
        {
            _builder.Register(new DeserializerStorage<SecondDeserializable>((d, r, p, v) => v.Value = r.ReadValue<int>()));
            Assert.Throws<InvalidOperationException>(() =>
            {
                _builder.Register(new DeserializerStorage<SecondDeserializable>((d, r, p, v) => v.Value = r.ReadValue<int>()));
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
