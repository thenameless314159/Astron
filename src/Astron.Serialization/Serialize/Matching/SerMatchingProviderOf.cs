using System.Reflection;
using Astron.Expressions.Matching;
using Astron.Serialization.Serialize.Expressions;
using Astron.Serialization.Serialize.Matching.Handlers;
using static Astron.Expressions.Helpers.SpecificationProvider;

namespace Astron.Serialization.Serialize.Matching
{
    public class SerMatchingProviderOf<TClass, TComp> : ISerMatchingProviderOf<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>
    {
        public IMatchingStrategy<PropertyInfo, TComp> Build()
        {
            var builder = new SerStrategyBuilder<TClass, TComp>();

            builder
                .Register(IsValueTypeValue.Or(IsString), new OnValueType<TClass, TComp>())
                .Register(IsNotValueTypeValue.And(IsNotString).And(IsNotGeneric.And(IsNotEnumerable)), new OnTypeValue<TClass, TComp>())
                .Register(IsValueTypeArray.Or(IsStringElement), new OnValueTypeArray<TClass, TComp>())
                .Register(IsNotValueTypeArray.And(IsNotStringElement), new OnTypeValueArray<TClass, TComp>())
                .Register(
                    IsGeneric
                        .And(IsGenericCollection)
                        .And(IsGenericValueType.Or(IsGenericString))
                        .And(IsNotGenericOfGeneric.Or(IsNotCollectionOfCollection)), 
                    new OnValueTypeCollection<TClass, TComp>())
                .Register(
                    IsGeneric
                        .And(IsGenericCollection)
                        .And(IsNotGenericValueType.And(IsNotGenericString))
                        .And(IsNotGenericOfGeneric.Or(IsNotCollectionOfCollection)), 
                    new OnTypeValueCollection<TClass, TComp>())
                .Register(
                    IsGeneric
                        .And(IsGenericCollection)
                        .And(IsGenericOfGeneric.And(IsCollectionOfCollection))
                        .And(IsValueTypeOfValueType.Or(IsStringOfString)),
                    new OnValueTypeCollectionOfCollection<TClass, TComp>())
                .Register(
                    IsGeneric
                        .And(IsGenericCollection)
                        .And(IsGenericOfGeneric.And(IsCollectionOfCollection))
                        .And(IsNotValueTypeOfValueType.And(IsNotStringOfString)), 
                    new OnTypeValueCollectionOfCollection<TClass, TComp>());

            return builder.Build();
        }
    }
}
