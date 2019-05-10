using System;
using System.Linq.Expressions;

namespace Astron.IoC.Cache
{
    internal static class InitializerCache<T>
    {
        private static Func<T> _initializer;
        private static bool _isAlreadyRegistered;

        public static Func<T> Value
        {
            get => _initializer;
            set
            {
                if (_isAlreadyRegistered)
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} initializer has already been registered in another {nameof(IContainer)}.");

                _initializer = value;
                _isAlreadyRegistered = true;
            }
        }

        public static void RegisterExpression()
        {
            if (_isAlreadyRegistered) return;
            Value = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
        }
    }
}
