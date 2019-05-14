using System.Reflection;
using Astron.Binary.Writer;
using Astron.Serialization.Serialize.Expressions;

namespace Astron.Serialization.Serialize.Matching.Handlers
{
    public class OnTypeValueCollectionOfCollection<TClass, TComp> : ISerMatchingHandler<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var genericType = pi.PropertyType.GenericTypeArguments[0];
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var writer = exprCompiler.GetParamIndex<IWriter>("writer");
            var serializer = exprCompiler.GetParamIndex<ISerializer>("serializer");

            var member = exprCompiler.Property(value, pi);
            var memberLen = exprCompiler.Property(member, pi.PropertyType.GetProperty("Count"));
            var list = exprCompiler.Variable(genericType, "list");
            var listCount = exprCompiler.Property(list, genericType.GetProperty("Count"));
            var element = exprCompiler.Variable(genericType.GenericTypeArguments[0], "element");
            var serializeElement = exprCompiler.SerializeValueFromParameter(element, writer, serializer);

            exprCompiler.Emit(exprCompiler.WriteValueFrom(memberLen, writer));
            exprCompiler.Emit(
                exprCompiler.Foreach(list, member,
                    exprCompiler.WriteValueFrom(listCount, writer),
                    exprCompiler.Foreach(element, list, serializeElement)));
        }
    }
}
