using System;
using Astron.Expressions.Helpers;
using Astron.IoC.Cache;
using Astron.IoC.Enums;

namespace Astron.IoC
{
    public class ContainerBuilder : IContainerBuilder
    {
        protected ImmutableDictionaryBuilder<Type, RegistrationMode> RegisteredTypesBuilder;

        public ContainerBuilder()
            => RegisteredTypesBuilder = new ImmutableDictionaryBuilder<Type, RegistrationMode>();

        public IContainerBuilder Register<TClass>() where TClass : class
        {
            var typeOfT = typeof(TClass);
            if(!RegisteredTypesBuilder.ContainsKey(typeOfT))
                InitializerCache<TClass>.RegisterExpression();

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.New;
            return this;
        }

        public IContainerBuilder Register<TClass>(TClass instance) where TClass : class
        {
            var typeOfT = typeof(TClass);
            if (!RegisteredTypesBuilder.ContainsKey(typeOfT))
                SingleInstanceCache<TClass>.Value = instance;

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.Singleton;
            return this;
        }

        public IContainerBuilder Register<TClass>(Func<TClass> initializer) where TClass : class
        {
            var typeOfT = typeof(TClass);
            if (!RegisteredTypesBuilder.ContainsKey(typeOfT))
                InitializerCache<TClass>.Value = initializer;

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.New;
            return this;
        }

        public IContainerBuilder Register<TClass>(Func<object[], TClass> valueInitializer) where TClass : class
        {
            var typeOfT = typeof(TClass);
            if (!RegisteredTypesBuilder.ContainsKey(typeOfT))
                ValuesInitializerCache<TClass>.Value = valueInitializer;

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.NewWithValues;
            return this;
        }

        public IContainerBuilder Register<TClass>(Func<IContainer, TClass> dependenciesInitializer) where TClass : class
        {
            var typeOfT = typeof(TClass);
            if (!RegisteredTypesBuilder.ContainsKey(typeOfT))
                DepInitializerCache<TClass>.Value = dependenciesInitializer;

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.NewWithDep;
            return this;
        }

        public IContainerBuilder Register<TClass>(Func<IContainer, object[], TClass> valuesWithDepInitializer) where TClass : class
        {
            var typeOfT = typeof(TClass);
            if (!RegisteredTypesBuilder.ContainsKey(typeOfT))
                ValuesWithDepInitializerCache<TClass>.Value = valuesWithDepInitializer;

            RegisteredTypesBuilder[typeOfT] = RegistrationMode.NewWithValuesAndDep;
            return this;
        }

        public IContainer Build()
        {
            RegisteredTypesBuilder.Build();
            return new Container(RegisteredTypesBuilder.Immutable);
        }
    }
}
