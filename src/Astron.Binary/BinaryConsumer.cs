using System;

namespace Astron.Binary
{
    public abstract class BinaryConsumer : IBinaryConsumer
    {
        private int _position;

        public int Count { get; }
        public int Consumed { get; private set; }
        
        public int Position
        {
            get => _position;
            private set
            {
                if (value > Count || value < 0) throw new ArgumentOutOfRangeException(
                    nameof(value), value, $"at position = {_position}.");
                _position = value;
            }
        }

        public int Remaining => Count - Position;

        protected BinaryConsumer(int count)
        {
            Count = count;
            Consumed =  0;
        }

        public void Seek(int position) => Position = position;

        public void Advance(int count)
        {
            Position += count;
            Consumed += count;
        }
    }
}
