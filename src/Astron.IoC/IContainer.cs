using System;
using System.Collections.Generic;
using Astron.IoC.Enums;

namespace Astron.IoC
{
    public interface IContainer
    {
        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is resolved according to its <see cref="RegistrationMode"/>.
        /// <see cref="T"/> must have been registered in the <see cref="IContainerBuilder"/> that built this <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance that you want to resolve.</typeparam>
        /// <returns>The resolved instance of type <see cref="T"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        T GetInstance<T>() where T : class;

        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is resolved according to its <see cref="RegistrationMode"/> and initialized with the specified <see cref="initializerValues"/>.
        /// <see cref="T"/> must have been registered in the <see cref="IContainerBuilder"/> that built this <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance that you want to resolve.</typeparam>
        /// <param name="initializerValues">The values that you want to pass in the target type constructor.</param>
        /// <returns>The resolved instance of type <see cref="T"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        T GetInstance<T>(params object[] initializerValues) where T : class;

        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is resolved according to its <see cref="RegistrationMode"/>.
        /// Returns false if <see cref="T"/> haven't been registered in the <see cref="IContainerBuilder"/> that built this <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance that you want to resolve.</typeparam>
        /// <param name="instance">The resolved instance of type <see cref="T"/>.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        bool TryGetInstance<T>(out T instance) where T : class;

        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is resolved according to its <see cref="RegistrationMode"/> and initialized with the specified <see cref="initializerValues"/>.
        /// Returns false if <see cref="T"/> haven't been registered in the <see cref="IContainerBuilder"/> that built this <see cref="IContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type of the instance that you want to resolve.</typeparam>
        /// <param name="instance">The resolved instance of type <see cref="T"/>.</param>
        /// <param name="initializerValues">he values that you want to pass in the target type constructor.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        bool TryGetInstance<T>(out T instance, params object[] initializerValues) where T : class;
    }
}
