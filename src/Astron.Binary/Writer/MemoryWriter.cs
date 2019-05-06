using System;
using System.Buffers;
using Astron.Binary.Cache;
using Astron.Size;

namespace Astron.Binary.Writer
{
    public class MemoryWriter : BinaryConsumer, IWriter
    {
        private readonly IMemoryOwner<byte> _bufferOwner;
        private readonly ISizing _sizing;

        public Memory<byte> GetBuffer()
            => Position == 0 ? Memory<byte>.Empty : _bufferOwner.Memory.Slice(0, Position);

        internal MemoryWriter(IMemoryOwner<byte> buffer, ISizing sizing, int size) : base(size)
        {
            _sizing = sizing;
            _bufferOwner = buffer;
        }

        public void WriteValue<T>(T value)
        {
            var isPrimitive = PrimitiveTypes.Primitives.Contains(typeof(T));
            var sizeOf = isPrimitive ? _sizing.SizeOf<T>() : _sizing.SizeOf(value);

            if (Remaining < sizeOf) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to write {typeof(T).Name}. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOf}.");

            if (!isPrimitive)
            {
                BinaryCache<T>.WriteValue(this, value);
                return;
            }

            var dst = _bufferOwner.Memory.Slice(Position, sizeOf);
            PrimitiveBinaryCache<T>.WriteValue(dst.Span, value);
            Advance(sizeOf);
        }

        public void WriteValues<T>(T[] values)
        {
            var buffer = _bufferOwner.Memory;
            var isPrimitive = PrimitiveTypes.Primitives.Contains(typeof(T));
            var sizeOfElem = isPrimitive ? _sizing.SizeOf<T>() : 0;

            if (!isPrimitive) // no size check bc it's checked on write calls from cache
            {
                foreach (var value in values) BinaryCache<T>.WriteValue(this, value);
                return;
            }

            var sizeOf = sizeOfElem * values.Length;

            if (Remaining < sizeOf) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to write {typeof(T).Name} array. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOf}.");

            var offset = Position;
            for (var i = 0; i < values.Length; i++, offset += sizeOfElem)
            {
                var dst = buffer.Slice(offset, sizeOfElem);
                PrimitiveBinaryCache<T>.WriteValue(dst.Span, values[i]);
            }
            Advance(sizeOf);
        }

        public void WriteValues<T>(Memory<T> memory)
        {
            var values = memory.Span;
            var buffer = _bufferOwner.Memory;
            var isPrimitive = PrimitiveTypes.Primitives.Contains(typeof(T));
            var sizeOfElem = isPrimitive ? _sizing.SizeOf<T>() : 0;

            if (!isPrimitive) // no size check bc it's checked on write calls from cache
            {
                foreach (var value in values) BinaryCache<T>.WriteValue(this, value);
                return;
            }

            var sizeOf = sizeOfElem * values.Length;

            if (Remaining < sizeOf) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to write {typeof(T).Name} array. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOf}.");

            var offset = Position;
            for (var i = 0; i < values.Length; i++, offset += sizeOfElem)
            {
                var dst = buffer.Slice(offset, sizeOfElem);
                PrimitiveBinaryCache<T>.WriteValue(dst.Span, values[i]);
            }
            Advance(sizeOf);
        }

        public void WriteValues<T>(ReadOnlyMemory<T> memory)
        {
            var values = memory.Span;
            var buffer = _bufferOwner.Memory;
            var isPrimitive = PrimitiveTypes.Primitives.Contains(typeof(T));
            var sizeOfElem = isPrimitive ? _sizing.SizeOf<T>() : 0;

            if (!isPrimitive) // no size check bc it's checked on write calls from cache
            {
                foreach (var value in values) BinaryCache<T>.WriteValue(this, value);
                return;
            }

            var sizeOf = sizeOfElem * values.Length;

            if (Remaining < sizeOf) throw new ArgumentOutOfRangeException(nameof(Remaining),
                $"Not enough bytes remaining in the buffer to write {typeof(T).Name} array. " +
                $"Position : {Position}, Length : {Count}, Remaining : {Remaining}, SizeOf : {sizeOf}.");

            var offset = Position;
            for (var i = 0; i < values.Length; i++, offset += sizeOfElem)
            {
                var dst = buffer.Slice(offset, sizeOfElem);
                PrimitiveBinaryCache<T>.WriteValue(dst.Span, values[i]);
            }
            Advance(sizeOf);
        }

        public void WriteBytes(byte[] data)
        {
            if (data.Length == 0) return;
            if (data.Length > Remaining) throw new ArgumentOutOfRangeException(nameof(data.Length));

            var dst = _bufferOwner.Memory.Slice(Position, data.Length);
            data.CopyTo(dst);
            Advance(data.Length);
        }

        public void WriteBytes(Memory<byte> data)
        {
            if (data.Length == 0) return;
            if (data.Length > Remaining) throw new ArgumentOutOfRangeException(nameof(data.Length));

            var dst = _bufferOwner.Memory.Slice(Position, data.Length);
            data.CopyTo(dst);
            Advance(data.Length);
        }

        public void WriteBytes(ReadOnlyMemory<byte> data)
        {
            if (data.Length == 0) return;
            if (data.Length > Remaining) throw new ArgumentOutOfRangeException(nameof(data.Length));

            var dst = _bufferOwner.Memory.Slice(Position, data.Length);
            data.CopyTo(dst);
            Advance(data.Length);
        }

        public void Dispose()
        {
            _bufferOwner.Dispose();
        }
    }
}
