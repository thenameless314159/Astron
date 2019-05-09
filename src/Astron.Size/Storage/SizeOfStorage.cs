using System;

namespace Astron.Size.Storage
{
    public class SizeOfStorage<T> : ISizeOfStorage<T>
    {
        public Func<ISizing, T, int> Calculate { get; }

        public SizeOfStorage(Func<ISizing, T, int> calculateFunc) => Calculate = calculateFunc;
    }
}
