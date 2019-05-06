namespace Astron.Binary.Storage
{
    public interface IBinaryStorage<T> : IReaderStorage<T>, IWriterStorage<T>
    {
    }
}
