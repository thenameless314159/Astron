using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Astron.Size.Tests")]
namespace Astron.Size.Cache
{
    internal class SizeOfCache<T>
    {
        private static bool _isAlreadyRegistered;
        private static Func<ISizing, T, int> _calculate;

        public static Func<ISizing, T, int> Calculate
        {
            get => _calculate ?? throw new KeyNotFoundException(
                $"{nameof(Calculate)} cache of {typeof(T).Name} haven't been registered in the current {nameof(ISizing)}.");
            set
            {
                if (_isAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(Calculate)} cache of {typeof(T).Name} has already been registered in another {nameof(ISizing)}.");

                _calculate = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
