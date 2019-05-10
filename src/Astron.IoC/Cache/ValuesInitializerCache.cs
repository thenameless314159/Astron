using System;

namespace Astron.IoC.Cache
{
    internal static class ValuesInitializerCache<T>
    {
        private static Func<object[], T> _valuesInitializer;
        private static bool _isAlreadyRegistered;

        public static Func<object[], T> Value
        {
            get => _valuesInitializer;
            set
            {
                if (_isAlreadyRegistered)
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} values initializer has already been registered in another {nameof(IContainer)}.");

                _valuesInitializer = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
