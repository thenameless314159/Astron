using System;
using System.Buffers;

namespace Astron.Memory
{
    public interface IMemoryPolicy
    {
        Memory<T> GetArray<T>(int size);
        IMemoryOwner<T> GetOwnedArray<T>(int size);
    }
}
