using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Serialization.Tests.Helpers
{
    public class EqualityComparerOf<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;

        public EqualityComparerOf(Func<T, T, bool> cmp) => _comparer = cmp;

        public bool Equals(T x, T y) => _comparer(x, y);

        public int GetHashCode(T obj) => obj.GetHashCode();
    }
}
