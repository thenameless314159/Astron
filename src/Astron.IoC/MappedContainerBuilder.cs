using System;
using System.Linq;
using Astron.Expressions.Helpers;
using Astron.IoC.Cache;
using Astron.IoC.Storage;

namespace Astron.IoC
{
    public class MappedContainerBuilder : IMappedContainerBuilder
    {
        protected ImmutableDictionaryBuilder<int, Type> RegTypesBuilder;
        protected ImmutableDictionaryBuilder<Type, AnonMappedInstanceStorage> RegAnonymousBuilder;

        public MappedContainerBuilder()
        {
            RegTypesBuilder = new ImmutableDictionaryBuilder<int, Type>();
            RegAnonymousBuilder = new ImmutableDictionaryBuilder<Type, AnonMappedInstanceStorage>();
        }

        public IMappedContainerBuilder Register<T>(int id)
        {
            if(!RegTypesBuilder.ContainsKey(id))
            {
                InitializerCache<T>.RegisterExpression();
                MappedInstanceCache<T>.Value = id;
            }

            var typeOfT = typeof(T);
            RegTypesBuilder[id] = typeOfT;
            RegAnonymousBuilder[typeOfT] = 
                new AnonMappedInstanceStorage(id, false, () => InitializerCache<T>.Value());

            var properties = PropertyHelper.SortPropertiesOf<T>();
            if (properties.Any()) return this;

            MappedInstanceCache<T>.IsEmpty = true;
            RegAnonymousBuilder[typeOfT] = new AnonMappedInstanceStorage(id, true, 
                () => InitializerCache<T>.Value()); return this;
        }

        public IMappedContainer Build()
        {
            RegTypesBuilder.Build();
            RegAnonymousBuilder.Build();

            return new MappedContainer(RegTypesBuilder.Immutable, RegAnonymousBuilder.Immutable);
        }
    }
}
