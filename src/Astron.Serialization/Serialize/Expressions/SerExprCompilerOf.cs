using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Astron.Binary.Writer;
using Astron.Expressions;

namespace Astron.Serialization.Serialize.Expressions
{
    public class SerExprCompilerOf<TClass> : ExpressionCompiler<Action<ISerializer, IWriter, TClass>>, 
        ISerExprCompilerOf<TClass>
    {
        private static readonly MethodInfo SerializeMethod = typeof(ISerializer).GetMethod("Serialize");
        private static readonly MethodInfo WriteValueMethod = typeof(IWriter).GetMethod("WriteValue");
        private static readonly MethodInfo WriteValuesMethod = typeof(IWriter).GetMethods()
            .First(m => m.Name == "WriteValues" && m.GetParameters().First().ParameterType.IsArray);

        public int WriteValueFrom(Type type, int toWriteIndex, int writerIndex)
            => Call(WriteValueMethod.MakeGenericMethod(type), writerIndex, toWriteIndex);

        public int WriteValueFrom(int memberIndex, int writerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;

            return Call(WriteValueMethod.MakeGenericMethod(memberType), writerIndex, memberIndex);
        }

        public int WriteValuesFrom(int memberIndex, int writerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type.GetElementType();

            return Call(WriteValuesMethod.MakeGenericMethod(memberType), writerIndex, memberIndex);
        }

        public int WriteValueFromParameter(int parameterIndex, int writerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var parameterType = (parameterExpr as ParameterExpression)?.Type;

            return Call(WriteValueMethod.MakeGenericMethod(parameterType), writerIndex, parameterIndex);
        }

        public int SerializeValueFrom(int memberIndex, int writerIndex, int serializerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;

            return Call(SerializeMethod.MakeGenericMethod(memberType), serializerIndex, writerIndex, memberIndex);
        }

        public int SerializeValueFromParameter(int parameterIndex, int writerIndex, int serializerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var parameterType = (parameterExpr as ParameterExpression)?.Type;

            return Call(SerializeMethod.MakeGenericMethod(parameterType), serializerIndex, writerIndex, parameterIndex);
        }
    }
}
