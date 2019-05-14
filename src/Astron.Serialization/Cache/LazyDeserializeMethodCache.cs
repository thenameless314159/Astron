using System;
using System.Runtime.CompilerServices;
using Astron.Binary.Reader;
using Astron.Memory;
using Astron.Serialization.Storage;

[assembly: InternalsVisibleTo("Astron.Serialization.Tests")]
namespace Astron.Serialization.Cache
{
    internal static class LazyDeserializeMethodCache<T>
    {
        private static Action<IDeserializer, IReader, IMemoryPolicy, T> _deserialize;

        public static Func<IDeserializerStorage<T>> Builder { get; set; }

        public static Action<IDeserializer, IReader, IMemoryPolicy, T> Deserialize
        {
            get
            {
                if (_deserialize != default) return _deserialize;
                var desStorage = Builder() ?? throw new ArgumentNullException(nameof(Builder));
                _deserialize = desStorage.Deserialize;
                return _deserialize;
            }
        }
    }
}
