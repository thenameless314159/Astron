using System.Runtime.CompilerServices;
using Astron.Size.Cache;
using Astron.Size.Storage;

[assembly: InternalsVisibleTo("Astron.Size.Tests")]
namespace Astron.Size
{
    public class SizingBuilder : ISizingBuilder
    {
        public SizingBuilder() => this
            .Register(StorageProvider.CreatePrimitive<bool>())
            .Register(StorageProvider.CreatePrimitive<byte>())
            .Register(StorageProvider.CreatePrimitive<sbyte>())
            .Register(StorageProvider.CreatePrimitive<short>())
            .Register(StorageProvider.CreatePrimitive<ushort>())
            .Register(StorageProvider.CreatePrimitive<int>())
            .Register(StorageProvider.CreatePrimitive<uint>())
            .Register(StorageProvider.CreatePrimitive<long>())
            .Register(StorageProvider.CreatePrimitive<ulong>())
            .Register(StorageProvider.CreatePrimitive<float>())
            .Register(StorageProvider.CreatePrimitive<double>());

        public ISizingBuilder Register<T>(ISizeStorage<T> storage)
        {
            SizeValueCache<T>.Value = storage.Value;
            return this;
        }

        public ISizingBuilder Register<T>(ISizeOfStorage<T> storage)
        {
            SizeOfCache<T>.Calculate = storage.Calculate;
            return this;
        }

        public ISizingBuilder Register<T>(ISizingStorage<T> storage) => this
            .Register((ISizeStorage<T>) storage)
            .Register((ISizeOfStorage<T>) storage);

        public ISizing Build() => new Sizing();
    }
}
