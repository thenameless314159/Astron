using System.Reflection;
using Astron.Binary.Reader;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Matching.Handlers
{
    public class OnTypeValueArray<TClass, TComp> : IDesMatchingHandler<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var elementType = pi.PropertyType.GetElementType();
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var reader = exprCompiler.GetParamIndex<IReader>("reader");
            var bufferPolicy = exprCompiler.GetParamIndex<IMemoryPolicy>("policy");
            var deserializer = exprCompiler.GetParamIndex<IDeserializer>("deserializer");

            var arrayLen = exprCompiler.Variable<int>("arrayLen");
            var i = exprCompiler.Variable<int>("i");

            var member = exprCompiler.Property(value, pi);
            var arrayAccess = exprCompiler.ArrayAccess(member, i);
            var deserialize = exprCompiler.DeserializeFrom(elementType, deserializer, reader);

            exprCompiler.EmitAssignReadValueFromParameter(arrayLen, reader);
            exprCompiler.EmitAssignNewArray(member, arrayLen, bufferPolicy);
            exprCompiler.Emit(
                exprCompiler.For(i, arrayLen, exprCompiler.Assign(arrayAccess, deserialize)));
        }
    }
}
