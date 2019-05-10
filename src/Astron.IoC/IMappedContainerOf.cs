using System.Collections.Generic;

namespace Astron.IoC
{
    public interface IMappedContainer<T>
    {
        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance of<see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder{T}"/>
        /// that built this <see cref="IMappedContainer{T}"/>.
        /// </summary>
        /// <param name="fromId">The identifier of the instance that you want to resolve.</param>
        /// <returns>The resolved instance of type <see cref="T"/>.</returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        T GetInstance(int fromId);

        /// <summary>
        /// Try to get an instance of <see cref="T"/>.
        /// </summary>
        /// <param name="fromId">The identifier of the instance that you want to resolve.</param>
        /// <param name="instance">The resolved instance of type <see cref="T"/>.</param>
        /// <returns></returns>
        bool TryGetInstance(int fromId, out T instance);

    }
}
