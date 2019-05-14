using System.Reflection;
using Astron.Binary.Writer;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching.Handlers
{
    public class OnTypeValue<TClass, TComp> : ISerMatchingHandler<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var writer = exprCompiler.GetParamIndex<IWriter>("writer");
            var serializer = exprCompiler.GetParamIndex<ISerializer>("serializer");
            var member = exprCompiler.Property(value, pi);

            exprCompiler.Emit(exprCompiler.SerializeValueFrom(member, writer, serializer));
        }
    }
}
