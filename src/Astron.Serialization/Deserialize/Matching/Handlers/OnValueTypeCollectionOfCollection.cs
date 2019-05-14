using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Astron.Binary.Reader;

namespace Astron.Serialization.Deserialize.Matching.Handlers
{
    public class OnValueTypeCollectionOfCollection<TClass, TComp> : IDesMatchingHandler<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        private static readonly Type List = typeof(List<>);

        public void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var elementType = pi.PropertyType.GenericTypeArguments[0].GenericTypeArguments[0];
            var innerListType = List.MakeGenericType(elementType);
            var listType = List.MakeGenericType(pi.PropertyType.GenericTypeArguments[0]);

            var addMethodList = listType.GetMethod("Add");
            var addMethodInnerList = innerListType.GetMethod("Add");
            var listCtor = listType.GetConstructors()
                .First(c => c.GetParameters().Any(p => p.ParameterType == typeof(int)));
            var innerListCtor = innerListType.GetConstructors()
                .First(c => c.GetParameters().Any(p => p.ParameterType == typeof(int)));

            var value = exprCompiler.GetParamIndex<TClass>("value");
            var reader = exprCompiler.GetParamIndex<IReader>("reader");
            var listCount = exprCompiler.Variable<int>("listCount");
            var innerListCount = exprCompiler.Variable<int>("innerListCount");
            var innerList = exprCompiler.Variable(innerListType, "innerList");
            var i = exprCompiler.Variable<int>("i");
            var j = exprCompiler.Variable<int>("j");

            var member = exprCompiler.Property(value, pi);

            var readElement = exprCompiler.ReadValueFrom(elementType, reader);
            var addReadElement = exprCompiler.Call(addMethodInnerList, innerList, readElement);
            var addInnerList = exprCompiler.Call(addMethodList, member, innerList);

            exprCompiler.EmitAssignReadValueFromParameter(listCount, reader);
            exprCompiler.EmitAssign(member, exprCompiler.New(listCtor, listCount));
            exprCompiler.Emit(
                exprCompiler.For(i, listCount, new []
                {
                    exprCompiler.Assign(innerListCount, exprCompiler.ReadValueFromParameter(innerListCount, reader)),
                    exprCompiler.Assign(innerList, exprCompiler.New(innerListCtor, innerListCount)),
                    exprCompiler.For(j, innerListCount, addReadElement),
                    addInnerList
                }));
        }
    }
}
