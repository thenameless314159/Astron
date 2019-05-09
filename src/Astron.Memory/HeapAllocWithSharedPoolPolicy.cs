using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace Astron.Memory
{
    public class HeapAllocWithSharedPoolPolicy : IMemoryPolicy
    {
        private static IMemoryOwner<T> EmptyOwner<T>() => SimpleMemoryOwner<T>.Empty;
        private static T[] Empty<T>() => Array.Empty<T>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Memory<T> GetArray<T>(int size)
        {
            if (size == 0) return Empty<T>();
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

            return new T[size];
        }

        public IMemoryOwner<T> GetOwnedArray<T>(int size)
        {
            if (size == 0) return EmptyOwner<T>();
            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));

            var arr = ArrayPool<T>.Shared.Rent(size);
            return new SharedPoolOwner<T>(arr, size);
        }
    }
}
