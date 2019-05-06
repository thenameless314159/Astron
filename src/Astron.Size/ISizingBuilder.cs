using Astron.Size.Storage;

namespace Astron.Size
{
    public interface ISizingBuilder
    {
        ISizingBuilder Register<T>(ISizeStorage<T> storage);
        ISizingBuilder Register<T>(ISizeOfStorage<T> storage);
        ISizingBuilder Register<T>(ISizingStorage<T> storage);
        ISizing Build();
    }
}
