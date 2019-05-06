using Astron.Binary.Storage;

namespace Astron.Binary
{
    public interface IBinaryBuilder
    {
        /// <summary>
        /// Register a <see cref="IWriterStorage{T}"/> of WriteValue method.
        /// The method is registered in a static-generic container,
        /// therefore you cannot register more than one method per <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to write with the specified WriteValue method.</typeparam>
        /// <param name="storage">The storage of WriteValue method to register.</param>
        /// <returns></returns>
        IBinaryBuilder Register<T>(IWriterStorage<T> storage);

        /// <summary>
        /// Register a <see cref="IReaderStorage{T}"/> of ReadValue method.
        /// The method is registered in a static-generic container,
        /// therefore you cannot register more than one method per <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to read with the specified ReadValue method.</typeparam>
        /// <param name="storage">The storage of ReadValue method to register.</param>
        /// <returns></returns>
        IBinaryBuilder Register<T>(IReaderStorage<T> storage);

        /// <summary>
        /// Register a <see cref="IBinaryStorage{T}"/> of WriteValue and ReadValue method.
        /// The methods are registered in a static-generic container,
        /// therefore you cannot register more than one method per <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to write and read with the specified WriteValue and ReadValue methods.</typeparam>
        /// <param name="storage">The storage of WriteValue and ReadValue methods to register.</param>
        /// <returns></returns>
        IBinaryBuilder Register<T>(IBinaryStorage<T> storage);

        /// <summary>
        /// Create a new instance of <see cref="IBinaryFactory"/>.
        /// </summary>
        /// <returns>A new instance of <see cref="IBinaryFactory"/>.</returns>
        IBinaryFactory Build();
    }
}
