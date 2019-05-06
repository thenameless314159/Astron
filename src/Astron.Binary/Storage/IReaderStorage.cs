using System;
using Astron.Binary.Reader;

namespace Astron.Binary.Storage
{
    public interface IReaderStorage<out T>
    {
        Func<IReader, T> ReadValue { get; }
    }
}
