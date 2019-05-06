using System;
using System.Collections.Generic;

namespace Astron.Binary.Writer
{
    public interface IWriter : IBinaryConsumer, IDisposable
    {
        /// <summary>
        /// Write a <see cref="value"/> of type <see cref="T"/> in the buffer and advance the position.
        /// </summary>
        /// <typeparam name="T">The type of the value to write in the buffer.</typeparam>
        /// <param name="value">The value to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteValue<T>(T value);

        /// <summary>
        /// Write an array of <see cref="T"/> in the buffer and advance the position.
        /// </summary>
        /// <typeparam name="T">The type of the value to write in the buffer.</typeparam>
        /// <param name="values">The values to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteValues<T>(T[] values);

        /// <summary>
        /// Write an array of <see cref="T"/> in the buffer and advance the position.
        /// </summary>
        /// <typeparam name="T">The type of the value to write in the buffer.</typeparam>
        /// <param name="memory">The values to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteValues<T>(Memory<T> memory);

        /// <summary>
        /// Write an array of <see cref="T"/> in the buffer and advance the position.
        /// </summary>
        /// <typeparam name="T">The type of the value to write in the buffer.</typeparam>
        /// <param name="memory">The values to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteValues<T>(ReadOnlyMemory<T> memory);

        /// <summary>
        /// Write a <see cref="byte"/> array in the buffer and advance the position.
        /// </summary>
        /// <param name="data">The data to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteBytes(byte[] data);

        /// <summary>
        /// Write a <see cref="byte"/> array in the buffer and advance the position.
        /// </summary>
        /// <param name="data">The data to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteBytes(Memory<byte> data);

        /// <summary>
        /// Write a <see cref="byte"/> array in the buffer and advance the position.
        /// </summary>
        /// <param name="data">The data to write in the buffer.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        void WriteBytes(ReadOnlyMemory<byte> data);

        /// <summary>
        /// Get the buffer which contains the written values.
        /// The buffer is sliced from start to current position.
        /// </summary>
        /// <returns>The buffer which contains the written values.</returns>
        Memory<byte> GetBuffer();
    }
}
