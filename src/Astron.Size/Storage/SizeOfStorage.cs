using System;
using System.Reflection;
using Astron.Expressions.Builder;
using Astron.Expressions.Matching;
using Astron.Size.Cache;
using Astron.Size.Expressions;
using Astron.Size.Matching;

namespace Astron.Size.Storage
{
    public class SizeOfStorage<T> : ISizeOfStorage<T>
    {
        public Func<ISizing, T, int> Calculate { get; }

        public SizeOfStorage(Func<ISizing, T, int> calculateFunc) => Calculate = calculateFunc;
    }
}
