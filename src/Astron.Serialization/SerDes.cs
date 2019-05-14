using Astron.Binary.Reader;
using Astron.Binary.Writer;

namespace Astron.Serialization
{
    public class SerDes : ISerDes
    {
        private ISerializer _serializer;
        private IDeserializer _deserializer;

        internal SerDes(ISerializer serializer, IDeserializer deserializer)
        {
            _serializer = serializer;
            _deserializer = deserializer;
        }

        public void Serialize<T>(IWriter writer, T value)
            => _serializer.Serialize(writer, value);

        public T Deserialize<T>(IReader reader) where T : new()
            => _deserializer.Deserialize<T>(reader);

        public void Deserialize<T>(IReader reader, T toDeserialize)
            => _deserializer.Deserialize(reader, toDeserialize);
    }
}
