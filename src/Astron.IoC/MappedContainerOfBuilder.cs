using Astron.Expressions.Helpers;

namespace Astron.IoC
{
    public class MappedContainerBuilder<T> : IMappedContainerBuilder<T>
    {
        protected ImmutableDictionaryBuilder<int, T> MappedInstancesBuilder;

        public MappedContainerBuilder()
            => MappedInstancesBuilder = new ImmutableDictionaryBuilder<int, T>();

        public IMappedContainerBuilder<T> Register(int id, T instance)
        {
            MappedInstancesBuilder[id] = instance;
            return this;
        }

        public IMappedContainer<T> Build()
        {
            MappedInstancesBuilder.Build();
            return new MappedContainer<T>(MappedInstancesBuilder.Immutable);
        }
    }
}
