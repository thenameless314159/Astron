using System;

namespace Astron.Size.Storage
{
    public class StringSizeStorage : ISizeOfStorage<string>
    {
        public Func<ISizing, string, int> Calculate => (s, v) => v.Length + 4;
    }
}
