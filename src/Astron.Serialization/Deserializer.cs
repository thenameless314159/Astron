using System.Runtime.CompilerServices;
using Astron.Binary.Reader;
using Astron.Memory;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;

[assembly: InternalsVisibleTo("Astron.Serialization.Tests")]
namespace Astron.Serialization
{
    public class Deserializer : IDeserializer
    {
        private readonly IMemoryPolicy _policy;

        internal Deserializer(IMemoryPolicy policy)
        {
            _policy = policy;
        }

        public T Deserialize<T>(IReader reader) where T : new()
        {
            var toDeserialize = new T();

            DeserializeMethodCache<T>.Deserialize(this, reader, _policy, toDeserialize);
            return toDeserialize;
        }

        public void Deserialize<T>(IReader reader, T toDeserialize)
            => DeserializeMethodCache<T>.Deserialize(this, reader, _policy, toDeserialize);
    }

    public class DeserializerBuilder : IDeserializerBuilder
    {
        private readonly IMemoryPolicy _policy;

        public DeserializerBuilder(IMemoryPolicy policy) => _policy = policy;

        public IDeserializerBuilder Register<T>(IDeserializerStorage<T> storage)
        {
            DeserializeMethodCache<T>.Deserialize = storage.Deserialize;
            return this;
        }

        public IDeserializer Build() => new Deserializer(_policy);
    }
}
