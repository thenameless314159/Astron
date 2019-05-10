using System;

namespace Astron.IoC
{
    public interface IMappedContainerBuilder<T>
    {
        /// <summary>
        /// Register an instance of <see cref="T"/> in the <see cref="IMappedContainer{T}"/> with its specified identifier.
        /// </summary>
        /// <typeparam name="T">The class to register in the <see cref="IContainer"/>.</typeparam>
        /// <param name="id">The id to map the <see cref="instance"/> with.</param>
        /// <param name="instance">The instance to map the <see cref="id"/> with.</param>
        /// <exception cref="InvalidOperationException">If <see cref="id"/> has already been registered
        /// in another <see cref="IMappedContainer{T}"/>.</exception>
        IMappedContainerBuilder<T> Register(int id, T instance);

        /// <summary>
        /// Build a <see cref="IMappedContainer"/> containing previously registered instances.
        /// </summary>
        /// <returns>A new <see cref="IMappedContainer{T}"/> instance containing previously registered instances.</returns>
        IMappedContainer<T> Build();
    }
}
