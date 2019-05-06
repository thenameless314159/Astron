using System.Runtime.CompilerServices;
using Astron.Size.BuiltIn;
using Astron.Size.Cache;
using Astron.Size.Storage;

[assembly: InternalsVisibleTo("Astron.Size.Tests")]
namespace Astron.Size
{
    public class SizingBuilder : ISizingBuilder
    {
        public SizingBuilder() => this
            .Register(new BoolSizeStorage())
            .Register(new Int8SizeStorage())
            .Register(new UInt8SizeStorage())
            .Register(new Int16SizeStorage())
            .Register(new UInt16SizeStorage())
            .Register(new Int32SizeStorage())
            .Register(new UInt32SizeStorage())
            .Register(new Int64SizeStorage())
            .Register(new UInt64SizeStorage())
            .Register(new FloatSizeStorage())
            .Register(new DoubleSizeStorage());

        public ISizingBuilder Register<T>(ISizeStorage<T> storage)
        {
            SizeValueCache<T>.Value = storage.Value;
            return this;
        }

        public ISizingBuilder Register<T>(ISizeOfStorage<T> storage)
        {
            SizeOfFuncCache<T>.SizeOf = storage.Calculate;
            return this;
        }

        public ISizingBuilder Register<T>(ISizingStorage<T> storage) => this
            .Register((ISizeStorage<T>) storage)
            .Register((ISizeOfStorage<T>) storage);

        public ISizing Build() => new Sizing();
    }
}
