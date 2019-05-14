using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Astron.Binary.Reader;
using Astron.Memory;

[assembly:InternalsVisibleTo("Astron.Serialization.Tests")]
namespace Astron.Serialization.Cache
{
    internal static class DeserializeMethodCache<T>
    {
        private static bool _isAlreadyRegistered;
        private static Action<IDeserializer, IReader, IMemoryPolicy, T> _deserialize;

        public static Action<IDeserializer, IReader, IMemoryPolicy, T> Deserialize
        {
            get => _deserialize ?? throw new KeyNotFoundException(
                       $"{nameof(Deserialize)} cache of {typeof(T).Name} haven't been registered in the current {nameof(IDeserializer)}.");
            set
            {
                if (_isAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(Deserialize)} cache of {typeof(T).Name} has already been registered in another {nameof(IDeserializer)}.");

                _deserialize = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
