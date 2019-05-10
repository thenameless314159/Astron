using System;
using Astron.Expressions.Builder;

namespace Astron.IoC
{
    public interface IMappedContainerBuilder : IBuilder<IMappedContainer>
    {
        /// <summary>
        /// Register a <see cref="T"/> in the <see cref="IMappedContainerBuilder"/> with its specified identifier.
        /// <see cref="T"/> must be a class with a parameter-less constructor.
        /// </summary>
        /// <typeparam name="T">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <param name="id">The id to map the <see cref="T"/> with.</param>
        /// <exception cref="InvalidOperationException">If <see cref="T"/> has already been registered
        /// in another <see cref="IMappedContainer"/>.</exception>
        IMappedContainerBuilder Register<T>(int id);
    }
}
