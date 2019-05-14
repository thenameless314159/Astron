using Astron.Binary.Reader;
using Astron.Expressions.Builder;
using Astron.Serialization.Storage;

namespace Astron.Serialization
{
    public interface IDeserializer
    {
        T Deserialize<T>(IReader reader) where T : new();
        void Deserialize<T>(IReader reader, T toDeserialize);
    }

    public interface IDeserializerBuilder : IBuilder<IDeserializer>
    {
        IDeserializerBuilder Register<T>(IDeserializerStorage<T> storage);
    }
}
