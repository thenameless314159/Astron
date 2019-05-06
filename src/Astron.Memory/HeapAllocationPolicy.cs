using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Astron.Memory
{
    public class HeapAllocationPolicy : IMemoryPolicy
    {
        private static T[] Empty<T>() => Array.Empty<T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Memory<T> GetArray<T>(int size)
        {
            if (size == 0) return Empty<T>();
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

            return new T[size];
        }

        public IMemoryOwner<T> GetOwnedArray<T>(int size)
            => new SimpleMemoryOwner<T>(GetArray<T>(size));
    }
}
