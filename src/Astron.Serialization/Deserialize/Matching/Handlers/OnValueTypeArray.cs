using System.Reflection;
using Astron.Binary.Reader;

namespace Astron.Serialization.Deserialize.Matching.Handlers
{
    public class OnValueTypeArray<TClass, TComp> : IDesMatchingHandler<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var reader = exprCompiler.GetParamIndex<IReader>("reader");
            var arrayLen = exprCompiler.Variable<int>("arrayLen");
            var member = exprCompiler.Property(value, pi);

            exprCompiler.EmitAssignReadValueFromParameter(arrayLen, reader);
            exprCompiler.EmitAssignReadValuesFrom(member, arrayLen, reader);
        }
    }
}
