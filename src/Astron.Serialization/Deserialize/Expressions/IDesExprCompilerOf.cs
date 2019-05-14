using System;
using Astron.Binary.Reader;
using Astron.Expressions;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Expressions
{
    public interface IDesExprCompilerOf<TClass> 
        : IExpressionCompiler<Action<IDeserializer, IReader, IMemoryPolicy, TClass>>
    {
        int ReadValueFromParameter(int parameterIndex, int readerIndex);
        int ReadValueFrom(int memberIndex, int readerIndex);
        int ReadValueFrom(Type type, int readerIndex);
        void EmitAssignReadValueFrom(int memberIndex, int readerIndex);
        void EmitAssignReadValueFromParameter(int parameterIndex, int readerIndex);

        int ReadValuesFrom(int memberIndex, int numberIndex, int readerIndex);
        int ReadValuesFrom(Type elementType, int numberIndex, int readerIndex);
        int ReadValuesFromParameter(int parameterIndex, int numberIndex, int readerIndex);
        void EmitAssignReadValuesFrom(int memberIndex, int numberIndex, int readerIndex);
        void EmitAssignReadValuesFromParameter(int parameterIndex, int numberIndex, int readerIndex);

        int DeserializeFrom(Type type, int deserializerIndex, int readerIndex);
        int DeserializeFrom(int memberIndex, int deserializerIndex, int readerIndex);
        void EmitAssignDeserializeFrom(int memberIndex, int deserializerIndex, int readerIndex);

        void EmitAssignNewArray(int memberIndex, int numberIndex, int bufferPolicyIndex);
    }
}
