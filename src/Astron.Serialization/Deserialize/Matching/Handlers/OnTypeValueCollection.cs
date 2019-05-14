using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Astron.Binary.Reader;

namespace Astron.Serialization.Deserialize.Matching.Handlers
{
    public class OnTypeValueCollection<TClass, TComp> : IDesMatchingHandler<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        private static readonly Type List = typeof(List<>);
        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var list = List.MakeGenericType(pi.PropertyType.GenericTypeArguments[0]);
            var addMethod = list.GetMethod("Add");
            var listCtor = list.GetConstructors()
                .First(c => c.GetParameters().Any(p => p.ParameterType == typeof(int)));

            var value = exprCompiler.GetParamIndex<TClass>("value");
            var reader = exprCompiler.GetParamIndex<IReader>("reader");
            var deserializer = exprCompiler.GetParamIndex<IDeserializer>("deserializer");

            var arrayLen = exprCompiler.Variable<int>("arrayLen");
            var i = exprCompiler.Variable<int>("i");
            var member = exprCompiler.Property(value, pi);

            var deserializeElement = 
                exprCompiler.DeserializeFrom(pi.PropertyType.GenericTypeArguments[0], deserializer, reader);
            var addReadElement = exprCompiler.Call(addMethod, member, deserializeElement);

            exprCompiler.EmitAssignReadValueFromParameter(arrayLen, reader);
            exprCompiler.EmitAssign(member, exprCompiler.New(listCtor, arrayLen));
            exprCompiler.Emit(exprCompiler.For(i, arrayLen, addReadElement));
        }
    }
}
