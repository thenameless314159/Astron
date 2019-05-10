using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Astron.IoC.Cache;
using Astron.IoC.Enums;

// ReSharper disable SwitchStatementMissingSomeCases

namespace Astron.IoC
{
    public class Container : IContainer
    {
        private readonly ImmutableDictionary<Type, RegistrationMode> _registeredTypes;

        internal Container(ImmutableDictionary<Type, RegistrationMode> registeredTypes)
            => _registeredTypes = registeredTypes;

        public T GetInstance<T>() where T : class
        {
            var typeOfT = typeof(T);
            if(!_registeredTypes.TryGetValue(typeOfT, out var regMode))
                throw new KeyNotFoundException($"{typeOfT.Name} is not registered in the current {nameof(Container)}.");

            switch (regMode)
            {
                case RegistrationMode.New: return InitializerCache<T>.Value();
                case RegistrationMode.Singleton: return SingleInstanceCache<T>.Value;
                case RegistrationMode.NewWithDep: return DepInitializerCache<T>.Value(this);
                default: throw new InvalidOperationException(
                        $"{typeOfT.Name} is not registered with an initializer (or as a singleton) in the current {nameof(Container)}.");
            }
        }

        public T GetInstance<T>(params object[] initializerValues) where T : class
        {
            var typeOfT = typeof(T);
            if (!_registeredTypes.TryGetValue(typeOfT, out var regMode))
                throw new KeyNotFoundException($"{typeOfT.Name} is not registered in the current {nameof(Container)}.");

            switch (regMode)
            {
                case RegistrationMode.NewWithValues: return ValuesInitializerCache<T>.Value(initializerValues);
                case RegistrationMode.NewWithValuesAndDep:
                    return ValuesWithDepInitializerCache<T>.Value(this, initializerValues);
                default: throw new InvalidOperationException(
                        $"{typeOfT.Name} is not registered with a values initializer in the current {nameof(Container)}.");
            }
        }

        public bool TryGetInstance<T>(out T instance) where T : class
        {
            instance = default;
            var typeOfT = typeof(T);
            if (!_registeredTypes.TryGetValue(typeOfT, out var regMode))
                return false;

            switch (regMode)
            {
                case RegistrationMode.New: instance = InitializerCache<T>.Value();
                    break;
                case RegistrationMode.Singleton: instance = SingleInstanceCache<T>.Value;
                    break;
                case RegistrationMode.NewWithDep: instance = DepInitializerCache<T>.Value(this);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"{typeOfT.Name} is not registered with an initializer (or as a singleton) in the current {nameof(Container)}.");
            }

            return true;
        }

        public bool TryGetInstance<T>(out T instance, params object[] initializerValues) where T : class
        {
            instance = default;
            var typeOfT = typeof(T);
            if (!_registeredTypes.TryGetValue(typeOfT, out var regMode))
                return false;

            switch (regMode)
            {
                case RegistrationMode.NewWithValues: instance = ValuesInitializerCache<T>.Value(initializerValues);
                    break;
                case RegistrationMode.NewWithValuesAndDep:
                    instance = ValuesWithDepInitializerCache<T>.Value(this, initializerValues);
                    break;
                default: throw new InvalidOperationException(
                        $"{typeOfT.Name} is not registered with a values initializer in the current {nameof(Container)}.");
            }

            return true;
        }
    }
}
