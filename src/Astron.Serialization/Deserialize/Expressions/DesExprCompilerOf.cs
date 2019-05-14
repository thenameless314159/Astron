using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Astron.Binary.Reader;
using Astron.Expressions;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Expressions
{
    public class DesExprCompilerOf<TClass> : ExpressionCompiler
        <Action<IDeserializer, IReader, IMemoryPolicy, TClass>>, IDesExprCompilerOf<TClass>
    {
        // IBufferPolicy returns a Memory<T>, forced to reallocate to use the array with ET
        private static MethodInfo GetToArrayMethod(Type t) => typeof(Memory<>).MakeGenericType(t).GetMethod("ToArray");
        private static readonly MethodInfo NewArrayMethod = typeof(IMemoryPolicy).GetMethod("GetArray");
        private static readonly MethodInfo ReadValueMethod = typeof(IReader).GetMethod("ReadValue");
        private static readonly MethodInfo ReadValuesMethod = typeof(IReader).GetMethod("ReadValues");
        private static readonly MethodInfo DeserializeMethod =
            typeof(IDeserializer).GetMethods().First(mi => mi.GetParameters().Length == 1);

        public int ReadValueFrom(Type type, int readerIndex)
            => Call(ReadValueMethod.MakeGenericMethod(type), readerIndex);

        public int ReadValuesFrom(Type elementType, int numberIndex, int readerIndex)
            => Call(ReadValuesMethod.MakeGenericMethod(elementType), readerIndex, numberIndex);

        public int ReadValueFrom(int memberIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;

            return ReadValueFrom(memberType, readerIndex);
        }

        public void EmitAssignReadValueFrom(int memberIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;
            var readMethod = ReadValueFrom(memberType, readerIndex);

            EmitAssign(memberIndex, readMethod);
        }

        public int ReadValueFromParameter(int parameterIndex, int readerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var parameterType = (parameterExpr as ParameterExpression)?.Type;

            return ReadValueFrom(parameterType, readerIndex);
        }

        public void EmitAssignReadValueFromParameter(int parameterIndex, int readerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var parameterType = (parameterExpr as ParameterExpression)?.Type;
            var readMethod = ReadValueFrom(parameterType, readerIndex);

            EmitAssign(parameterIndex, readMethod);
        }

        public int ReadValuesFrom(int memberIndex, int numberIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var elementType = (memberExpr as MemberExpression)?.Type.GetElementType();

            return ReadValuesFrom(elementType, numberIndex, readerIndex);
        }

        public void EmitAssignReadValuesFrom(int memberIndex, int numberIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var elementType = (memberExpr as MemberExpression)?.Type.GetElementType();
            var readMethod = ReadValuesFrom(elementType, numberIndex, readerIndex);;

            EmitAssign(memberIndex, readMethod);
        }

        public int ReadValuesFromParameter(int parameterIndex, int numberIndex, int readerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var elementType = (parameterExpr as ParameterExpression)?.Type.GetElementType();

            return ReadValuesFrom(elementType, numberIndex, readerIndex);
        }

        public void EmitAssignReadValuesFromParameter(int parameterIndex, int numberIndex, int readerIndex)
        {
            var parameterExpr = Expressions[parameterIndex];
            var elementType = (parameterExpr as ParameterExpression)?.Type.GetElementType();
            var readMethod = ReadValuesFrom(elementType, numberIndex, readerIndex);

            EmitAssign(parameterIndex, readMethod);
        }

        public int DeserializeFrom(Type type, int deserializerIndex, int readerIndex)
            => Call(DeserializeMethod.MakeGenericMethod(type), deserializerIndex, readerIndex);

        public int DeserializeFrom(int memberIndex, int deserializerIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;

            return DeserializeFrom(memberType, deserializerIndex, readerIndex);
        }

        public void EmitAssignDeserializeFrom(int memberIndex, int deserializerIndex, int readerIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type;

            var callDes = Call(DeserializeMethod.MakeGenericMethod(memberType), deserializerIndex, readerIndex);
            EmitAssign(memberIndex, callDes);
        }

        public void EmitAssignNewArray(int memberIndex, int numberIndex, int bufferPolicyIndex)
        {
            var memberExpr = Expressions[memberIndex];
            var memberType = (memberExpr as MemberExpression)?.Type.GetElementType();
            var newArray = Call(NewArrayMethod.MakeGenericMethod(memberType), bufferPolicyIndex, numberIndex);

            EmitAssign(memberIndex, Call(GetToArrayMethod(memberType), newArray));
        }
    }
}
