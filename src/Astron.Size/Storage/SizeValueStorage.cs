namespace Astron.Size.Storage
{
    public class SizeValueStorage<T> : ISizeStorage<T>
    {
        public int Value { get; }

        public SizeValueStorage(int value) => Value = value;
    }
}
