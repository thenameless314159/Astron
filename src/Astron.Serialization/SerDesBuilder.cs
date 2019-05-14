using Astron.Memory;
using Astron.Serialization.Storage;

namespace Astron.Serialization
{
    public class SerDesBuilder : ISerDesBuilder
    {
        private readonly ISerializerBuilder _serBuilder;
        private readonly IDeserializerBuilder _desBuilder;

        public SerDesBuilder(IMemoryPolicy policy)
        {
            _serBuilder = new SerializerBuilder();
            _desBuilder = new DeserializerBuilder(policy);
        }

        public ISerDesBuilder Register<T>(ISerDesStorage<T> storage) => this
            .Register((ISerializerStorage<T>) storage)
            .Register((IDeserializerStorage<T>) storage);

        public ISerDesBuilder Register<T>(ISerializerStorage<T> serStorage)
        {
            _serBuilder.Register(serStorage);
            return this;
        }

        public ISerDesBuilder Register<T>(IDeserializerStorage<T> desStorage)
        {
            _desBuilder.Register(desStorage);
            return this;
        }

        public ISerDes Build() => new SerDes(_serBuilder.Build(), _desBuilder.Build()); 
    }
}
