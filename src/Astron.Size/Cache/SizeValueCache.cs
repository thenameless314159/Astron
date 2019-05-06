using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Size.Cache
{
    internal static class SizeValueCache<T>
    {
        private static bool _isAlreadyRegistered;
        private static int _value;

        public static int Value
        {
            get => _value > 0 ? _value : throw new KeyNotFoundException(
                $"{nameof(Value)} cache of {typeof(T).Name} haven't been registered in the current {nameof(ISizing)}.");
            set
            {
                if (_isAlreadyRegistered) return;
                _value = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
