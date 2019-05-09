using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Astron.Size.Cache;

namespace Astron.Size
{
    public class Sizing : ISizing
    {
        internal Sizing()
        {
        }

        public int SizeOf<T>() => SizeValueCache<T>.Value;
        public int SizeOf<T>(T value) => SizeOfCache<T>.Calculate(this, value);

        public int SizeOf<T>(T[] values)
            => values.Sum(value => SizeOfCache<T>.Calculate(this, value));

        public int SizeOf<T>(Memory<T> values)
        {
            var size = 0;
            foreach (var value in values.Span)
                size += SizeOfCache<T>.Calculate(this, value);

            return size;
        }

        public int SizeOf<T>(ReadOnlyMemory<T> values)
        {
            var size = 0;
            foreach (var value in values.Span)
                size += SizeOfCache<T>.Calculate(this, value);

            return size;
        }
    }
}
