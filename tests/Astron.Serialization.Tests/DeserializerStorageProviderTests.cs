using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Astron.Binary;
using Astron.Memory;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;
using Astron.Serialization.Tests.Helpers;
using Astron.Serialization.Tests.Models;
using Astron.Size;
using Xunit;

namespace Astron.Serialization.Tests
{
    public class DeserializerStorageProviderTests
    {
        private readonly ValueComparer _valueComparer = new ValueComparer();
        private readonly IBinaryFactory _binFactory = new BinaryBuilder(new SizingBuilder().Build()).Build();
        private readonly IDeserializer _emptyDes = new Deserializer(_policy);
        private static readonly IMemoryPolicy _policy = new HeapAllocationPolicy();
        private static readonly IDeserializer _valueDes = new DeserializerBuilder(_policy)
            .Register(DeserializerStorageProvider.CreateCompiled<Value>())
            .Build();

        [Fact]
        public void CreateCompiled_ShouldHandleValueType()
        {
            var value = new Value { B = 8, S = 16, I = 32, L = 64 };

            var writer = _binFactory.Get(15);
            value.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var desValue = new Value();
            _valueDes.Deserialize(reader, desValue);

            Assert.True(_valueComparer.Equals(value, desValue));
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeArray()
        {
            var value = new Values { Array = new[] { 1, 2, 3, 4, 5, 6 } };

            var writer = _binFactory.Get(28);
            writer.WriteValue(value.Array.Length);
            writer.WriteValues(value.Array);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<Values>();
            var desValue = new Values();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            Assert.Equal(value.Array, desValue.Array);
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValue()
        {
            var value = new TypeValue { Primitive = new Value { B = 8, S = 16, I = 32, L = 64 } };

            var writer = _binFactory.Get(15);
            value.Primitive.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<TypeValue>();
            var desValue = new TypeValue();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            Assert.True(_valueComparer.Equals(value.Primitive, desValue.Primitive));
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
            writer.WriteValue(value.Primitives.Length);
            foreach (var v in value.Primitives)
                v.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<TypeValues>();
            var desValue = new TypeValues();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            Assert.True(!value.Primitives.Where((t, i) => !_valueComparer.Equals(t, desValue.Primitives[i])).Any());
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeCollection()
        {
            var value = new ValueCollection { Collection = new List<int>(6) { 1, 2, 3, 4, 5, 6 } };

            var writer = _binFactory.Get(28);
            value.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<ValueCollection>();
            var desValue = new ValueCollection();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            Assert.Equal(value.Collection, desValue.Collection);
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueCollection()
        {
            var value = new TypeValueCollection
            {
                Collection = new List<Value>(6)
                {
                    new Value { B = 8, S = 16, I = 32, L = 64 },
                    new Value { B = 8, S = 16, I = 32, L = 64 }
                }
            };

            var writer = _binFactory.Get(34);
            value.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<TypeValueCollection>();
            var desValue = new TypeValueCollection();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            for (var i = 0; i < 2; i++)
                Assert.True(_valueComparer.Equals(value.Collection[i], desValue.Collection[i]));
        }

        [Fact]
        public void CreateCompiled_ShouldHandleValueTypeCollectionOfCollection()
        {
            var value = new ValueCollectionOfCollection
            {
                Collection = new List<List<int>>(2)
                {
                    new List<int>(3) {1,2,3},
                    new List<int>(3) {4,5,6}
                }
            };

            var writer = _binFactory.Get(36);
            value.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<ValueCollectionOfCollection>();
            var desValue = new ValueCollectionOfCollection();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            Assert.Equal(value.Collection[0], desValue.Collection[0]);
            Assert.Equal(value.Collection[1], desValue.Collection[1]);
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueCollectionOfCollection()
        {
            var value = new TypeValueCollectionOfCollection
            {
                Collection = new List<List<Value>>(2)
                {
                    new List<Value>(1) { new Value { B = 8, S = 16, I = 32, L = 64 } },
                    new List<Value>(1) { new Value { B = 8, S = 16, I = 32, L = 64 } }
                }
            };

            var writer = _binFactory.Get(42);
            value.Serialize(writer);

            var reader = _binFactory.Get(writer.GetBuffer());
            var compiledStorage = DeserializerStorageProvider.CreateCompiled<TypeValueCollectionOfCollection>();
            var desValue = new TypeValueCollectionOfCollection();
            compiledStorage.Deserialize(_emptyDes, reader, _policy, desValue);

            for (var i = 0; i < 2; i++)
                Assert.True(_valueComparer.Equals(value.Collection[i][0], desValue.Collection[i][0]));
        }

        [Fact]
        public void CreateLazy_ShouldBuildOnFirstCall()
        {
            var builder = new DeserializerBuilder(_policy);
            Assert.Null(LazyDeserializeMethodCache<LazyPrimitive>.Builder); // ensure nothing is set yet

            var storage = DeserializerStorageProvider.CreateLazy<LazyPrimitive>();
            Assert.NotNull(LazyDeserializeMethodCache<LazyPrimitive>.Builder); // should be set now
            Assert.Throws<KeyNotFoundException>(() => DeserializeMethodCache<LazyPrimitive>.Deserialize); // still not registered, should be null

            builder.Register(storage);
            Assert.NotNull(DeserializeMethodCache<LazyPrimitive>.Deserialize); // should be set now
        }

        class LazyPrimitive { public int Value { get; set; } }
    }
}
