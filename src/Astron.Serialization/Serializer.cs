using System.Runtime.CompilerServices;
using Astron.Binary.Writer;
using Astron.Serialization.Cache;
using Astron.Serialization.Storage;

[assembly: InternalsVisibleTo("Astron.Serialization.e2e")]
namespace Astron.Serialization
{
    public class Serializer : ISerializer
    {
        internal Serializer()
        {
        }

        public void Serialize<T>(IWriter writer, T value)
            => SerializeMethodCache<T>.Serialize(this, writer, value);
    }

    public class SerializerBuilder : ISerializerBuilder
    {
        public ISerializerBuilder Register<T>(ISerializerStorage<T> storage)
        {
            SerializeMethodCache<T>.Serialize = storage.Serialize;
            return this;
        }

        public ISerializer Build() => new Serializer();
    }
}
