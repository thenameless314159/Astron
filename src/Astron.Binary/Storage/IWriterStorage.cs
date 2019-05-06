using System;
using Astron.Binary.Writer;

namespace Astron.Binary.Storage
{
    public interface IWriterStorage<in T>
    {
        Action<IWriter, T> WriteValue { get; }
    }
}
