using System.Collections.Generic;
using System.Collections.Immutable;

namespace Astron.IoC
{
    public class MappedContainer<T> : IMappedContainer<T>
    {
        private ImmutableDictionary<int, T> _mappedInstances;

        internal MappedContainer(ImmutableDictionary<int, T> mappedInstances)
            => _mappedInstances = mappedInstances;

        public T GetInstance(int fromId)
        {
            if(!_mappedInstances.ContainsKey(fromId))
                throw new KeyNotFoundException(
                    $"{typeof(T)} instance with id:{fromId} haven't been registered in this {nameof(IMappedContainer<T>)}.");

            return _mappedInstances[fromId];
        }

        public bool TryGetInstance(int fromId, out T instance)
            => _mappedInstances.TryGetValue(fromId, out instance);
    }
}
