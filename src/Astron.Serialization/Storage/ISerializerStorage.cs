using System;
using Astron.Binary.Writer;

namespace Astron.Serialization.Storage
{
    public interface ISerializerStorage<in T>
    {
        Action<ISerializer, IWriter, T> Serialize { get; }
    }
}
