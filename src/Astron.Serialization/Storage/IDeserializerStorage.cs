using System;
using Astron.Binary.Reader;
using Astron.Memory;

namespace Astron.Serialization.Storage
{
    public interface IDeserializerStorage<in T>
    {
        Action<IDeserializer, IReader, IMemoryPolicy, T> Deserialize { get; }
    }
}
