using System;
using System.Runtime.CompilerServices;
using Astron.Binary.Writer;
using Astron.Serialization.Storage;

[assembly: InternalsVisibleTo("Astron.Serialization.Tests")]
namespace Astron.Serialization.Cache
{
    internal static class LazySerializeMethodCache<T>
    {
        private static Action<ISerializer, IWriter, T> _serialize;

        public static Func<ISerializerStorage<T>> Builder { get; set; }

        public static Action<ISerializer, IWriter, T> Serialize
        {
            get
            {
                if (_serialize != default) return _serialize;
                var serStorage = Builder() ?? throw new ArgumentNullException(nameof(Builder));
                _serialize = serStorage.Serialize;
                return _serialize;
            }
        }
    }
}
