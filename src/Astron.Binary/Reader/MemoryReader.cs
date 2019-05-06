using System;
using System.Runtime.CompilerServices;
using Astron.Binary.Cache;

// ReSharper disable FieldCanBeMadeReadOnly.Local

[assembly:InternalsVisibleTo("Astron.Binary.Tests")]
namespace Astron.Binary.Reader
{
    public class MemoryReader : BinaryConsumer, IReader
    {
        private ReadOnlyMemory<byte> _buffer;

        internal MemoryReader(ReadOnlyMemory<byte> buffer) : base(buffer.Length)
        {
            _buffer = buffer;
        }

        public T ReadValue<T>()
        {
            if (!PrimitiveTypes.Primitives.Contains(typeof(T)))
                return BinaryCache<T>.ReadValue(this);

            var sizeOfT = PrimitiveBinaryCache<T>.SizeOf;

            if (Remaining < sizeOfT) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to read {typeof(T).Name}. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOfT}.");

            var src = _buffer.Slice(Position, sizeOfT);
            var value = PrimitiveBinaryCache<T>.ReadValue(src.Span);
            Advance(sizeOfT);
            return value;
        }

        public T[] ReadValues<T>(int count)
        {
            if(count == 0) return Array.Empty<T>();

            var value = new T[count];

            if (!PrimitiveTypes.Primitives.Contains(typeof(T)))
            {
                for (var i = 0; i < count; i++) value[i] = BinaryCache<T>.ReadValue(this);
                return value;
            }

            var sizeOfElem = PrimitiveBinaryCache<T>.SizeOf;
            var sizeOfArray = sizeOfElem * count;

            if (Remaining < sizeOfArray) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to read {typeof(T).Name} array with elements count : {count}. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOfArray}.");

            var offset = Position;
            for (var i = 0; i < count; i++, offset += sizeOfElem)
            {
                var src = _buffer.Slice(offset, sizeOfElem);
                value[i] = PrimitiveBinaryCache<T>.ReadValue(src.Span);
            }

            Advance(sizeOfArray);
            return value;
        }

        public ReadOnlyMemory<byte> GetSlice() => _buffer.Slice(Position);

        public ReadOnlyMemory<byte> GetSlice(int count) => _buffer.Slice(Position, count);
    }
}
