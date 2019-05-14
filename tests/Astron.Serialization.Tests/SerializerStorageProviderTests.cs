using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Astron.Binary;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;
using Astron.Serialization.Tests.Models;
using Astron.Size;
using Astron.Size.Storage;
using Xunit;

namespace Astron.Serialization.Tests
{
    public class SerializerStorageProviderTests
    {
        private readonly IBinaryFactory _binFactory = new BinaryBuilder(new SizingBuilder().Build()).Build();
        private readonly ISerializer _emptySer = new Serializer();
        private static readonly ISerializer _valueSer = new SerializerBuilder()
            .Register(SerializerStorageProvider.CreateCompiled<Value>())
            .Build();

        [Fact]
        public void CreateCompiled_ShouldHandleValueType()
        {
            var value = new Value { B = 8, S = 16, I = 32, L = 64 };

            var writer = _binFactory.Get(15);
            var writerSer = _binFactory.Get(15);

            value.Serialize(writer);
            _valueSer.Serialize(writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeArray()
        {
            var value = new Values { Array = new[] { 1, 2, 3, 4, 5, 6 } };

            var writer = _binFactory.Get(28);
            var writerSer = _binFactory.Get(28);

            writer.WriteValue(value.Array.Length);
            writer.WriteValues(value.Array);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<Values>();
            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValue()
        {
            var value = new TypeValue { Primitive = new Value { B = 8, S = 16, I = 32, L = 64 } };

            var writer = _binFactory.Get(15);
            var writerSer = _binFactory.Get(15);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<TypeValue>();
            value.Primitive.Serialize(writer);
            compiledStorage.Serialize(_valueSer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueArray()
        {
            var value = new TypeValues
            {
                Primitives = new[]
                {
                    new Value { B = 8, S = 16, I = 32, L = 64 },
                    new Value { B = 8, S = 16, I = 32, L = 64 }
                }
            };

            var writer = _binFactory.Get(34);
            var writerSer = _binFactory.Get(34);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<TypeValues>();
            writer.WriteValue(value.Primitives.Length);
            foreach (var v in value.Primitives)
                v.Serialize(writer);

            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeCollection()
        {
            var value = new ValueCollection()
            {
                Collection = new List<int>(5) { 1, 2, 3, 4, 5, 6 }
            };

            var writer = _binFactory.Get(28);
            var writerSer = _binFactory.Get(28);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<ValueCollection>();
            value.Serialize(writer);
            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueCollection()
        {
            var value = new TypeValueCollection()
            {
                Collection = new List<Value>(2)
                {
                    new Value { B = 8, S = 16, I = 32, L = 64 },
                    new Value { B = 8, S = 16, I = 32, L = 64 }
                }
            };

            var writer = _binFactory.Get(34);
            var writerSer = _binFactory.Get(34);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<TypeValueCollection>();
            value.Serialize(writer);
            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeCollectionOfCollection()
        {
            var value = new ValueCollectionOfCollection()
            {
                Collection = new List<List<int>>(2)
                {
                    new List<int>(3) {1,2,3},
                    new List<int>(3) {4,5,6}
                }
            };

            var writer = _binFactory.Get(36);
            var writerSer = _binFactory.Get(36);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<ValueCollectionOfCollection>();
            value.Serialize(writer);
            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueCollectionOfCollection()
        {
            var value = new TypeValueCollectionOfCollection()
            {
                Collection = new List<List<Value>>(2)
                {
                    new List<Value>(1) { new Value { B = 8, S = 16, I = 32, L = 64 } },
                    new List<Value>(1) { new Value { B = 8, S = 16, I = 32, L = 64 } }
                }
            };

            var writer = _binFactory.Get(42);
            var writerSer = _binFactory.Get(42);

            var compiledStorage = SerializerStorageProvider.CreateCompiled<TypeValueCollectionOfCollection>();
            value.Serialize(writer);
            compiledStorage.Serialize(_emptySer, writerSer, value);

            Assert.Equal(
                writer.GetBuffer().ToArray(),
                writerSer.GetBuffer().ToArray());
        }

        [Fact]
        public void CreateLazy_ShouldBuildOnFirstCall()
        {
            var builder = new SerializerBuilder();
            Assert.Null(LazySerializeMethodCache<LazyPrimitive>.Builder); // ensure nothing is set yet

            var storage = SerializerStorageProvider.CreateLazy<LazyPrimitive>();
            Assert.NotNull(LazySerializeMethodCache<LazyPrimitive>.Builder); // should be set now
            Assert.Throws<KeyNotFoundException>(() => SerializeMethodCache<LazyPrimitive>.Serialize); // still not registered, should be null

            builder.Register(storage);
            Assert.NotNull(SerializeMethodCache<LazyPrimitive>.Serialize); // should be set now
        }

        class LazyPrimitive { public int Value { get; set; } }
    }
}
