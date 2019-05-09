using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Astron.Size.Storage;

[assembly: InternalsVisibleTo("Astron.Size.Tests")]
namespace Astron.Size.Cache
{
    internal static class LazySizeOfCache<TClass>
    {
        private static Func<ISizing, TClass, int> _calculate;

        public static Func<ISizeOfStorage<TClass>> Builder { get; set; }

        public static Func<ISizing, TClass, int> Calculate
        {
            get
            {
                if (_calculate != default) return _calculate;
                var sizeStorage = Builder() ?? throw new ArgumentNullException(nameof(Builder));
                _calculate = sizeStorage.Calculate;
                return _calculate;
            }
        }
    }
}
