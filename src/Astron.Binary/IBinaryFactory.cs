using System;
using Astron.Binary.Reader;
using Astron.Binary.Writer;

namespace Astron.Binary
{
    public interface IBinaryFactory
    {
        /// <summary>
        /// Create a new instance of <see cref="IReader"/> with the specified <see cref="buffer"/>.
        /// </summary>
        /// <param name="buffer">The buffer to pass in the <see cref="IReader"/>.</param>
        /// <returns>A new instance of <see cref="IReader"/>.</returns>
        IReader Get(byte[] buffer);

        /// <summary>
        /// Create a new instance of <see cref="IReader"/> with the specified <see cref="buffer"/>.
        /// </summary>
        /// <param name="buffer">The buffer to pass in the <see cref="IReader"/>.</param>
        /// <returns>A new instance of <see cref="IReader"/>.</returns>
        IReader Get(Memory<byte> buffer);

        /// <summary>
        /// Create a new instance of <see cref="IReader"/> with the specified <see cref="buffer"/>.
        /// </summary>
        /// <param name="buffer">The buffer to pass in the <see cref="IReader"/>.</param>
        /// <returns>A new instance of <see cref="IReader"/>.</returns>
        IReader Get(ReadOnlyMemory<byte> buffer);

        /// <summary>
        /// Create a new instance of <see cref="IWriter"/> with its specified <see cref="length"/>.
        /// </summary>
        /// <param name="length">The length to pass in the <see cref="IWriter"/>.</param>
        /// <returns>A new instance of <see cref="IWriter"/>.</returns>
        IWriter Get(int length);
    }
}
