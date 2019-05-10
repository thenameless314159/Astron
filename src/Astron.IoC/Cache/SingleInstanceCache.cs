using System;

namespace Astron.IoC.Cache
{
    internal static class SingleInstanceCache<T>
    {
        private static T _instance;
        private static bool _isAlreadyRegistered;

        public static T Value
        {
            get => _instance;
            set
            {
                if (_isAlreadyRegistered)
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} has already been registered in another {nameof(IContainer)} as a singleton.");

                _instance = value;
                _isAlreadyRegistered = true;
            }
        }
    }
}
