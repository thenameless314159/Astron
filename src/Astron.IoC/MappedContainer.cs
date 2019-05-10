using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Astron.IoC.Cache;
using Astron.IoC.Storage;

namespace Astron.IoC
{
    public class MappedContainer : IMappedContainer
    {
        private readonly ImmutableDictionary<int, Type> _registeredTypes;
        private readonly ImmutableDictionary<Type, AnonMappedInstanceStorage> _registeredAnonymous;

        internal MappedContainer(
            ImmutableDictionary<int, Type> registeredTypes,
            ImmutableDictionary<Type, AnonMappedInstanceStorage> registeredAnonymous)
        {
            _registeredTypes = registeredTypes;
            _registeredAnonymous = registeredAnonymous;
        }

        public T GetInstance<T>()
        {
            var typeOfT = typeof(T);
            if (!_registeredTypes.ContainsValue(typeOfT))
                throw new KeyNotFoundException(
                    $"{typeOfT.Name} is not registered in this {nameof(MappedContainer)}.");

            return InitializerCache<T>.Value();
        }

        public T GetInstance<T>(int fromId)
        {
            if(!_registeredTypes.ContainsKey(fromId))
                throw new KeyNotFoundException(
                    $"Type with id:{fromId} is not registered in this {nameof(MappedContainer)}.");

            return InitializerCache<T>.Value();
        }

        public object GetInstance(int fromId)
        {
            if (!_registeredTypes.TryGetValue(fromId, out var value))
                throw new KeyNotFoundException(
                    $"Type with id:{fromId} is not registered in this {nameof(MappedContainer)}.");

            return _registeredAnonymous[value].Initializer();
        }

        public int GetId<T>()
        {
            var typeOfT = typeof(T);
            if (!_registeredTypes.ContainsValue(typeOfT))
                throw new KeyNotFoundException(
                    $"{typeOfT} is not registered in this {nameof(MappedContainer)}.");

            return MappedInstanceCache<T>.Value;
        }

        public int GetId<T>(T fromInstance) => GetId<T>();

        public int GetId(object fromInstance)
        {
            var typeOfInstance = fromInstance.GetType();
            if (!_registeredTypes.ContainsValue(typeOfInstance))
                throw new KeyNotFoundException(
                    $"{typeOfInstance} is not registered in this {nameof(MappedContainer)}.");

            return _registeredAnonymous[typeOfInstance].Id;
        }

        public bool IsEmpty<T>()
        {
            var typeOfT = typeof(T);
            if (!_registeredTypes.ContainsValue(typeOfT))
                throw new KeyNotFoundException(
                    $"{typeOfT} is not registered in this {nameof(MappedContainer)}.");

            return MappedInstanceCache<T>.IsEmpty;
        }

        public bool IsEmpty<T>(T instance) => IsEmpty<T>(); 

        public bool IsEmpty(int id)
        {
            if (!_registeredTypes.TryGetValue(id, out var value))
                throw new KeyNotFoundException(
                    $"Type with id:{id} is not registered in this {nameof(MappedContainer)}.");

            return _registeredAnonymous[value].IsEmpty;
        }

        public bool TryGetInstance<T>(int fromId, out T instance)
        {
            instance = default;
            if (!_registeredTypes.ContainsKey(fromId))
                return false;

            instance = InitializerCache<T>.Value();
            return true;
        }

        public bool TryGetInstance(int fromId, out object instance)
        {
            instance = default;
            if (!_registeredTypes.TryGetValue(fromId, out var value))
                return false;

            instance = _registeredAnonymous[value].Initializer();
            return true;
        }

        public bool Contains(int id) => _registeredTypes.ContainsKey(id);
    }
}
