using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Size.Cache
{
    internal class SizeOfFuncCache<T>
    {
        private static bool _isAlreadyRegistered;
        private static Func<ISizing, T, int> _sizeOf;

        public static Func<ISizing, T, int> SizeOf
        {
            get => _sizeOf ?? throw new KeyNotFoundException(
                $"{nameof(SizeOf)} cache of {typeof(T).Name} haven't been registered in the current {nameof(ISizing)}.");
            set
            {
                if (_isAlreadyRegistered) throw new InvalidOperationException(
                    $"{nameof(SizeOf)} cache of {typeof(T).Name} has already been registered in another {nameof(ISizing)}.");

                _sizeOf = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
