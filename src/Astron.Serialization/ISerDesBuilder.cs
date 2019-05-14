using Astron.Serialization.Storage;

namespace Astron.Serialization
{
    public interface ISerDesBuilder
    {
        ISerDesBuilder Register<T>(ISerDesStorage<T> storage);
        ISerDesBuilder Register<T>(ISerializerStorage<T> serStorage);
        ISerDesBuilder Register<T>(IDeserializerStorage<T> desStorage);

        ISerDes Build();
    }
}
