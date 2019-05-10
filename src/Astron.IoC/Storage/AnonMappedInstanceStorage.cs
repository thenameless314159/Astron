using System;

namespace Astron.IoC.Storage
{
    public class AnonMappedInstanceStorage
    {
        public int Id { get; }
        public bool IsEmpty { get; }
        public Func<object> Initializer { get; }

        public AnonMappedInstanceStorage(int id, bool isEmpty, Func<object> initializer)
        {
            Id = id;
            IsEmpty = isEmpty;
            Initializer = initializer;
        }
    }
}
