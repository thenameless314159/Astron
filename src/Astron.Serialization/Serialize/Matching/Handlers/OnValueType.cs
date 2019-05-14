using System.Reflection;
using Astron.Binary.Writer;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching.Handlers
{
    public class OnValueType<TClass, TComp> : ISerMatchingHandler<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var writer = exprCompiler.GetParamIndex<IWriter>("writer");
            var member = exprCompiler.Property(value, pi);

            exprCompiler.Emit(exprCompiler.WriteValueFrom(member, writer));
        }
    }
}
