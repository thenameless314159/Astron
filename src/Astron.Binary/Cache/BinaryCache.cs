using System;
using System.Collections.Generic;
using Astron.Binary.Reader;
using Astron.Binary.Writer;

namespace Astron.Binary.Cache
{
    internal static class BinaryCache<T>
    {
        private static bool _isReaderAlreadyRegistered;
        private static bool _isWriterAlreadyRegistered;

        private static Func<IReader, T> _reader;
        private static Action<IWriter, T> _writer;

        public static Func<IReader, T> ReadValue
        {
            get => _reader ?? throw new KeyNotFoundException(
                $"{nameof(ReadValue)} cache of {typeof(T).Name} haven't been registered in the current {nameof(IReader)}.");
            set
            {
                if (_isReaderAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(ReadValue)} cache of {typeof(T).Name} has already been registered in another {nameof(IReader)}.");

                _reader = value;
                _isReaderAlreadyRegistered = true;
            }
        }

        public static Action<IWriter, T> WriteValue
        {
            get => _writer ?? throw new KeyNotFoundException(
                $"{nameof(WriteValue)} cache of {typeof(T).Name} haven't been registered in the current {nameof(IWriter)}.");
            set
            {
                if (_isWriterAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(WriteValue)} cache of {typeof(T).Name} has already been registered in another {nameof(IWriter)}.");

                _writer = value;
                _isWriterAlreadyRegistered = true;
            }
        }
    }
}
