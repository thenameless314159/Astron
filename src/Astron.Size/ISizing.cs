using System;

namespace Astron.Size
{
    public interface ISizing
    {
        int SizeOf<T>();
        int SizeOf<T>(T value);
        int SizeOf<T>(T[] values);
        int SizeOf<T>(Memory<T> values);
        int SizeOf<T>(ReadOnlyMemory<T> values);
    }
}
