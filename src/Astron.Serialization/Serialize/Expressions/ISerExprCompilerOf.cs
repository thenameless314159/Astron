using System;
using Astron.Binary.Writer;
using Astron.Expressions;

namespace Astron.Serialization.Serialize.Expressions
{
    public interface ISerExprCompilerOf<TClass>
        : IExpressionCompiler<Action<ISerializer, IWriter, TClass>>
    {
        int WriteValueFrom(Type type, int toWriteIndex, int writerIndex);
        int WriteValueFrom(int memberIndex, int writerIndex);
        int WriteValuesFrom(int memberIndex, int writerIndex);
        int WriteValueFromParameter(int parameterIndex, int writerIndex);

        int SerializeValueFrom(int memberIndex, int writerIndex, int serializerIndex);
        int SerializeValueFromParameter(int parameterIndex, int writerIndex, int serializerIndex);
    }
}
