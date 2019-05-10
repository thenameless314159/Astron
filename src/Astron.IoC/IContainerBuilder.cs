using System;
using Astron.Expressions.Builder;

namespace Astron.IoC
{
    public interface IContainerBuilder : IBuilder<IContainer>
    {
        /// <summary>
        /// Register a <see cref="TClass"/> in the <see cref="IContainer"/>.
        /// <see cref="TClass"/> must be a class with a parameter-less constructor.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>() where TClass : class;

        /// <summary>
        /// Register a single instance of <see cref="TClass"/> (singleton) in the <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>(TClass instance) where TClass : class;

        /// <summary>
        /// Register a <see cref="TClass"/> in the <see cref="IContainer"/> with a custom <see cref="initializer"/>.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>(Func<TClass> initializer) where TClass : class;

        /// <summary>
        /// Register a <see cref="TClass"/> in the <see cref="IContainer"/> with a <see cref="valueInitializer"/>.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>(Func<object[], TClass> valueInitializer) where TClass : class;

        /// <summary>
        /// Register a <see cref="TClass"/> in the <see cref="IContainer"/> with a <see cref="dependenciesInitializer"/>.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>(Func<IContainer, TClass> dependenciesInitializer) where TClass : class;

        /// <summary>
        /// Register a <see cref="TClass"/> in the <see cref="IContainer"/> with a <see cref="valuesWithDepInitializer"/>.
        /// </summary>
        /// <typeparam name="TClass">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <exception cref="InvalidOperationException">If <see cref="TClass"/> has already been registered
        /// in another <see cref="IContainer"/>.</exception>
        IContainerBuilder Register<TClass>(Func<IContainer,object[], TClass> valuesWithDepInitializer)
            where TClass : class;
    }
}
