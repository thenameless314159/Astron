using System.Reflection;
using Astron.Binary.Reader;

namespace Astron.Serialization.Deserialize.Matching.Handlers
{
    public class OnTypeValue<TClass, TComp> : IDesMatchingHandler<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var value = exprCompiler.GetParamIndex<TClass>("value");
            var reader = exprCompiler.GetParamIndex<IReader>("reader");
            var deserializer = exprCompiler.GetParamIndex<IDeserializer>("deserializer");
            var member = exprCompiler.Property(value, pi);

            exprCompiler.EmitAssignDeserializeFrom(member, deserializer, reader);
        }
    }
}
