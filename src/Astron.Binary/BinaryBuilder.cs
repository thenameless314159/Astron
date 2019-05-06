using Astron.Binary.Cache;
using Astron.Binary.Storage;
using Astron.Memory;
using Astron.Size;

namespace Astron.Binary
{
    public class BinaryBuilder : IBinaryBuilder
    {
        private readonly IMemoryPolicy _policy;
        private readonly ISizing _sizing;

        public BinaryBuilder(ISizing sizing, IMemoryPolicy policy = default, 
            Endianness endianness = Endianness.LittleEndian)
        {
            _policy = policy ?? new HeapAllocationPolicy();
            _sizing = sizing;

            if (PrimitiveBinaryCacheBuilder.IsAlreadySet) return;

            if (endianness == Endianness.LittleEndian)
                PrimitiveBinaryCacheBuilder.RegisterAllLittleEndian();
            else PrimitiveBinaryCacheBuilder.RegisterAllBigEndian();
        }

        public IBinaryBuilder Register<T>(IWriterStorage<T> storage)
        {
            BinaryCache<T>.WriteValue = storage.WriteValue;
            return this;
        }

        public IBinaryBuilder Register<T>(IReaderStorage<T> storage)
        {
            BinaryCache<T>.ReadValue = storage.ReadValue;
            return this;
        }

        public IBinaryBuilder Register<T>(IBinaryStorage<T> storage) => this
            .Register((IWriterStorage<T>)storage)
            .Register((IReaderStorage<T>)storage);

        public IBinaryFactory Build() => new BinaryFactory(_policy, _sizing);
    }
}
