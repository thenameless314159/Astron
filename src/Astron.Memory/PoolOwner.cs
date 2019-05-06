using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Astron.Memory
{
    public abstract class PoolOwner<T> : IMemoryOwner<T>
    {
        private readonly int _length;
        private T[] _oversized;

        public Memory<T> Memory => new Memory<T>(GetArray(), 0, _length);

        protected PoolOwner(T[] oversized, int length)
        {
            if (length > oversized.Length) throw new ArgumentOutOfRangeException(nameof(length));

            _length = length;
            _oversized = oversized;
        }

        protected abstract void ReturnToPool(T[] array);

        protected T[] GetArray() =>
            Interlocked.CompareExchange(ref _oversized, null, null)
            ?? throw new ObjectDisposedException(ToString());

        public void Dispose()
        {
            var arr = Interlocked.Exchange(ref _oversized, null);
            if (arr != null) ReturnToPool(arr);
        }
    }
}
