using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Astron.Memory
{
    internal sealed class SharedPoolOwner<T> : PoolOwner<T>
    {
        public SharedPoolOwner(T[] oversized, int length) : base(oversized, length)
        {
        }

        protected override void ReturnToPool(T[] array) => ArrayPool<T>.Shared.Return(array);
    }
}
