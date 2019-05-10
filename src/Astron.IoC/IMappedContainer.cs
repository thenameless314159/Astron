using System.Collections.Generic;

namespace Astron.IoC
{
    public interface IMappedContainer
    {
        bool Contains(int id);

        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is always resolved as a new one.
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type that you want to resolve.</typeparam>
        /// <returns>The resolved instance of type <see cref="T"/></returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        T GetInstance<T>();

        /// <summary>
        /// Get an instance of <see cref="T"/>.
        /// The instance is always resolved as a new one.
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type that you want to resolve.</typeparam>
        /// <param name="fromId">The identifier of the type that you want to resolve.</param>
        /// <returns>The resolved instance of type <see cref="T"/></returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        T GetInstance<T>(int fromId);

        bool TryGetInstance<T>(int fromId, out T instance);
        
        /// <summary>
        /// Get an instance of type <see cref="fromId"/>.
        /// The instance is always resolved as a new one.
        /// The type <see cref="fromId"/> must have been registered in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <param name="fromId">The identifier of the type that you want to resolve.</param>
        /// <returns>The resolved instance of type <see cref="fromId"/></returns>
        /// <exception cref="KeyNotFoundException">If the type has not been registered with its identifier.</exception>
        object GetInstance(int fromId);

        bool TryGetInstance(int fromId, out object instance);

        /// <summary>
        /// Get the identifier of the specified <see cref="T"/>.
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to provide to get its identifier.</typeparam>
        /// <returns>The identifier resolved from <see cref="T"/></returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        int GetId<T>();

        /// <summary>
        /// Get the identifier of the specified <see cref="T"/>.
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to provide to get its identifier.</typeparam>
        /// <returns>The identifier resolved from <see cref="T"/></returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        int GetId<T>(T fromInstance);

        /// <summary>
        /// Get the identifier <see cref="fromInstance"/> specified.
        /// The type <see cref="fromInstance"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <returns>The identifier resolved <see cref="fromInstance"/></returns>
        /// <exception cref="KeyNotFoundException">If the type has not been registered with its identifier.</exception>
        int GetId(object fromInstance);

        /// <summary>
        /// Tells you if the <see cref="T"/> contains less than 1 properties in its class definition.
        /// (basically tells you if it must be serialized or not)
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to provide to determine its emptiness.</typeparam>
        /// <returns>Whether <see cref="T"/> is a property-less type.</returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        bool IsEmpty<T>();

        /// <summary>
        /// Tells you if the <see cref="T"/> contains less than 1 properties in its class definition.
        /// (basically tells you if it must be serialized or not)
        /// <see cref="T"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <typeparam name="T">The type to provide to determine its emptiness.</typeparam>
        /// <returns>Whether <see cref="T"/> is a property-less type.</returns>
        /// <exception cref="KeyNotFoundException">If <see cref="T"/> has not been registered with its identifier.</exception>
        bool IsEmpty<T>(T instance);

        /// <summary>
        /// Tells you if the type get from <see cref="id"/> contains less than 1 properties in its class definition.
        /// (basically tells you if it must be serialized or not)
        /// The type from <see cref="id"/> must have been registered with its identifier in the <see cref="IMappedContainerBuilder"/>
        /// that built this <see cref="IMappedContainer"/>.
        /// </summary>
        /// <param name="id">>The identifier of the type that you wan to resolve to determine its emptiness.</param>
        /// <returns>Whether the type from <see cref="id"/> is a property-less type.</returns>
        /// <exception cref="KeyNotFoundException">If the type has not been registered with its identifier.</exception>
        bool IsEmpty(int id);
    }
}
