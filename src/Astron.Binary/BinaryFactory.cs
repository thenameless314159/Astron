using System;
using Astron.Binary.Reader;
using Astron.Binary.Writer;
using Astron.Memory;
using Astron.Size;

namespace Astron.Binary
{
    public class BinaryFactory : IBinaryFactory
    {
        private readonly IMemoryPolicy _policy;
        private readonly ISizing _sizing;

        internal BinaryFactory(IMemoryPolicy policy, ISizing sizing)
        {
            _policy = policy;
            _sizing = sizing;
        }

        public IReader Get(byte[] buffer) => new MemoryReader(buffer);

        public IReader Get(Memory<byte> buffer) => new MemoryReader(buffer);

        public IReader Get(ReadOnlyMemory<byte> buffer) => new MemoryReader(buffer);

        public IWriter Get(int length) => new MemoryWriter(_policy.GetOwnedArray<byte>(length), _sizing, length);
    }
}
