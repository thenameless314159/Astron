using System;
using Astron.Binary.Reader;
using Astron.Memory;

namespace Astron.Serialization.Storage
{
    public class DeserializerStorage<T> : IDeserializerStorage<T>
    {
        public Action<IDeserializer, IReader, IMemoryPolicy, T> Deserialize { get; }

        public DeserializerStorage(Action<IDeserializer, IReader, IMemoryPolicy, T> deserialize)
            => Deserialize = deserialize;
    }
}
