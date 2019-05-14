using System.Reflection;
using Astron.Binary.Writer;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching.Handlers
{
    public class OnValueTypeCollection<TClass, TComp> : ISerMatchingHandler<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var writer = exprCompiler.GetParamIndex<IWriter>("writer");

            var member = exprCompiler.Property(value, pi);
            var memberLen = exprCompiler.Property(member, pi.PropertyType.GetProperty("Count"));
            var element = exprCompiler.Variable(pi.PropertyType.GenericTypeArguments[0], "element");
            var writeElement = exprCompiler.WriteValueFromParameter(element, writer);

            exprCompiler.Emit(exprCompiler.WriteValueFrom(memberLen, writer));
            exprCompiler.Emit(exprCompiler.Foreach(element, member, writeElement));
        }
    }
}
