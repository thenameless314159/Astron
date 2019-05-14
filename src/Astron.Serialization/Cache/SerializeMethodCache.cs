using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Astron.Binary.Writer;

[assembly: InternalsVisibleTo("Astron.Serialization.Tests")]
namespace Astron.Serialization.Cache
{
    internal static class SerializeMethodCache<T>
    {
        private static bool _isAlreadyRegistered;
        private static Action<ISerializer, IWriter, T> _serialize;

        public static Action<ISerializer, IWriter, T> Serialize
        {
            get => _serialize ?? throw new KeyNotFoundException(
                       $"{nameof(Serialize)} cache of {typeof(T).Name} haven't been registered in the current {nameof(ISerializer)}.");
            set
            {
                if (_isAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(Serialize)} cache of {typeof(T).Name} has already been registered in another {nameof(ISerializer)}.");

                _serialize = value;
                _isAlreadyRegistered = true;
            }
        }

    }
}
