using System;

namespace Astron.Size.Storage
{
    public interface ISizeOfStorage<in T>
    {
        Func<ISizing, T, int> Calculate { get; }
    }
}
