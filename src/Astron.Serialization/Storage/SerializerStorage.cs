using System;
using Astron.Binary.Writer;

namespace Astron.Serialization.Storage
{
    public class SerializerStorage<T> : ISerializerStorage<T>
    {
        public Action<ISerializer, IWriter, T> Serialize { get; }

        public SerializerStorage(Action<ISerializer, IWriter, T> serialize)
            => Serialize = serialize;
    }
}
