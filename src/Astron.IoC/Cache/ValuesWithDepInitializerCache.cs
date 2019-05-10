using System;

namespace Astron.IoC.Cache
{
    internal static class ValuesWithDepInitializerCache<T>
    {
        private static Func<IContainer, object[], T> _valuesWithDepInitializer;
        private static bool _isAlreadyRegistered;

        public static Func<IContainer, object[], T> Value
        {
            get => _valuesWithDepInitializer;
            set
            {
                if (_isAlreadyRegistered)
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} values with dependencies initializer has already been registered in another {nameof(IContainer)}.");

                _valuesWithDepInitializer = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
