using Astron.Binary.Writer;
using Astron.Serialization.Storage;

namespace Astron.Serialization
{
    public interface ISerializer
    {
        void Serialize<T>(IWriter writer, T value);
    }

    public interface ISerializerBuilder
    {
        ISerializerBuilder Register<T>(ISerializerStorage<T> storage);
        ISerializer Build();
    }
}
