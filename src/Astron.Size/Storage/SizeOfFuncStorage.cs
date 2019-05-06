using System;

namespace Astron.Size.Storage
{
    public class SizeOfFuncStorage<T> : ISizeOfStorage<T>
    {
        public Func<ISizing, T, int> Calculate { get; }

        public SizeOfFuncStorage(Func<ISizing, T, int> calculateFunc) => Calculate = calculateFunc;
    }
}
