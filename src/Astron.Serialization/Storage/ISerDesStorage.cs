namespace Astron.Serialization.Storage
{
    public interface ISerDesStorage<in T> : ISerializerStorage<T>, IDeserializerStorage<T>
    {
    }
}
