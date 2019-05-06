using System;
using System.Collections.Generic;

namespace Astron.Binary.Reader
{
    public interface IReader : IBinaryConsumer
    {
        /// <summary>
        /// Read a <see cref="T"/> from the buffer, advance the position and return the result.
        /// </summary>
        /// <typeparam name="T">The type of the value to read from the buffer.</typeparam>
        /// <returns>The value that have been read from the buffer.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        T ReadValue<T>();

        /// <summary>
        /// Read an array of <see cref="T"/> from the buffer, advance the position and return the result.
        /// </summary>
        /// <typeparam name="T">The type of the values to read from the buffer.</typeparam>
        /// <param name="count">The number of elements to read from the buffer.</param>
        /// <returns>The values that have been read from the buffer.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        T[] ReadValues<T>(int count);

        /// <summary>
        /// Get a slice from the buffer currently read at the actual position to the end.
        /// </summary>
        /// <returns></returns>
        ReadOnlyMemory<byte> GetSlice();

        /// <summary>
        /// Get a slice of length <see cref="count"/> from the buffer currently read at the actual position.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ReadOnlyMemory<byte> GetSlice(int count);
    }
}
