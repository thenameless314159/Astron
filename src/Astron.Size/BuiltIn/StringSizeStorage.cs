using System;
using Astron.Size.Storage;

namespace Astron.Size.BuiltIn
{
    public class StringSizeStorage : ISizeOfStorage<string>
    {
        public Func<ISizing, string, int> Calculate => (s, v) => v.Length + 4;
    }
}
