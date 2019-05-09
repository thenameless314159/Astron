using System;
using System.Collections.Generic;


namespace Astron.Binary.Cache
{
    internal static class PrimitiveBinaryCache<T>
    {
        private static bool _isAlreadyRegistered;
        public delegate T ReadDelegate(ReadOnlySpan<byte> src);
        public delegate void WriteDelegate(Span<byte> dst, T value);

        private static int _sizeOf;
        private static ReadDelegate _reader;
        private static WriteDelegate _writer;

        public static void CompleteRegistration() => _isAlreadyRegistered = true;

        public static int SizeOf
        {
            get => _sizeOf;
            set
            {
                if (_isAlreadyRegistered) return;
                _sizeOf = value;
            }
        }

        public static ReadDelegate ReadValue
        {
            get => _reader ?? throw new KeyNotFoundException(
                       $"{nameof(ReadValue)} haven't been registered in the current {nameof(PrimitiveBinaryCache<T>)}.");
            set
            {
                if (_isAlreadyRegistered) return;
                _reader = value;
            }
        }

        public static WriteDelegate WriteValue
        {
            get => _writer ?? throw new KeyNotFoundException(
                       $"{nameof(WriteValue)} haven't been registered in the current {nameof(PrimitiveBinaryCache<T>)}.");
            set
            {
                if (_isAlreadyRegistered) return;

                _writer = value;
            }
        }
    }
}
