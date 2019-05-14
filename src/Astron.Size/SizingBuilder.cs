using System.Runtime.CompilerServices;
using Astron.Size.Cache;
using Astron.Size.Storage;

[assembly: InternalsVisibleTo("Astron.Size.Tests")]
namespace Astron.Size
{
    public class SizingBuilder : ISizingBuilder
    {
        public SizingBuilder() => this
            .Register(SizeStorageProvider.CreatePrimitive<bool>())
            .Register(SizeStorageProvider.CreatePrimitive<byte>())
            .Register(SizeStorageProvider.CreatePrimitive<sbyte>())
            .Register(SizeStorageProvider.CreatePrimitive<short>())
            .Register(SizeStorageProvider.CreatePrimitive<ushort>())
            .Register(SizeStorageProvider.CreatePrimitive<int>())
            .Register(SizeStorageProvider.CreatePrimitive<uint>())
            .Register(SizeStorageProvider.CreatePrimitive<long>())
            .Register(SizeStorageProvider.CreatePrimitive<ulong>())
            .Register(SizeStorageProvider.CreatePrimitive<float>())
            .Register(SizeStorageProvider.CreatePrimitive<double>());

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
