using System;

namespace Astron.IoC.Cache
{
    internal static class DepInitializerCache<T>
    {
        private static Func<IContainer, T> _dependenciesInitializer;
        private static bool _isAlreadyRegistered;

        public static Func<IContainer, T> Value
        {
            get => _dependenciesInitializer;
            set
            {
                if (_isAlreadyRegistered)
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} dependencies initializer has already been registered in another {nameof(IContainer)}.");

                _dependenciesInitializer = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
