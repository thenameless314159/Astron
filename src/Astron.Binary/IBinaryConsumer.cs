namespace Astron.Binary
{
    public interface IBinaryConsumer
    {
        /// <summary>
        /// Get the length of the buffer contained in the current <see cref="IBinaryConsumer"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Get the number of bytes that have been consumed in the current <see cref="IBinaryConsumer"/>.
        /// </summary>
        int Consumed { get; }

        /// <summary>
        /// Get the position in the buffer where the <see cref="IBinaryConsumer"/> currently is.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Get the number of bytes remaining in the current <see cref="IBinaryConsumer"/>.
        /// </summary>
        int Remaining { get; }

        /// <summary>
        /// Set the <see cref="Position"/> of the current <see cref="IBinaryConsumer"/> at <see cref="position"/>.
        /// </summary>
        /// <param name="position">The position to set.</param>
        void Seek(int position);

        /// <summary>
        /// Advance from the current position to the <see cref="count"/>.
        /// </summary>
        /// <param name="count">The number of bytes to advance the <see cref="Position"/>.</param>
        void Advance(int count);
    }
}
