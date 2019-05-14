using System.Reflection;
using Astron.Expressions.Helpers;
using Astron.Expressions.Matching;

namespace Astron.Serialization.Deserialize.Matching
{
    public class DesMatchingProviderOf<TClass, TComp> : IDesMatchingProviderOf<TClass, TComp>
        where TComp : Expressions.IDesExprCompilerOf<TClass>
    {
        public IMatchingStrategy<PropertyInfo, TComp> Build()
        {
            var builder = new DesStrategyBuilder<TClass, TComp>();

            builder
                .Register(SpecificationProvider.IsValueTypeValue.Or(SpecificationProvider.IsString), new Handlers.OnValueType<TClass, TComp>())
                .Register(SpecificationProvider.IsNotValueTypeValue.And(SpecificationProvider.IsNotString).And(SpecificationProvider.IsNotGeneric.And(SpecificationProvider.IsNotEnumerable)), new Handlers.OnTypeValue<TClass, TComp>())
                .Register(SpecificationProvider.IsValueTypeArray.Or(SpecificationProvider.IsStringElement), new Handlers.OnValueTypeArray<TClass, TComp>())
                .Register(SpecificationProvider.IsNotValueTypeArray.And(SpecificationProvider.IsNotStringElement), new Handlers.OnTypeValueArray<TClass, TComp>())
                .Register(
                    SpecificationProvider.IsGeneric
                        .And(SpecificationProvider.IsGenericCollection)
                        .And(SpecificationProvider.IsGenericValueType.Or(SpecificationProvider.IsGenericString))
                        .And(SpecificationProvider.IsNotGenericOfGeneric.Or(SpecificationProvider.IsNotCollectionOfCollection)), 
                    new Handlers.OnValueTypeCollection<TClass, TComp>())
                .Register(
                    SpecificationProvider.IsGeneric
                        .And(SpecificationProvider.IsGenericCollection)
                        .And(SpecificationProvider.IsNotGenericValueType.And(SpecificationProvider.IsNotGenericString))
                        .And(SpecificationProvider.IsNotGenericOfGeneric.Or(SpecificationProvider.IsNotCollectionOfCollection)), 
                new Handlers.OnTypeValueCollection<TClass, TComp>())
                .Register(
                    SpecificationProvider.IsGeneric
                        .And(SpecificationProvider.IsGenericCollection)
                        .And(SpecificationProvider.IsGenericOfGeneric.And(SpecificationProvider.IsCollectionOfCollection))
                        .And(SpecificationProvider.IsValueTypeOfValueType.Or(SpecificationProvider.IsStringOfString)),
                    new Handlers.OnValueTypeCollectionOfCollection<TClass, TComp>())
                .Register(
                    SpecificationProvider.IsGeneric
                        .And(SpecificationProvider.IsGenericCollection)
                        .And(SpecificationProvider.IsGenericOfGeneric.And(SpecificationProvider.IsCollectionOfCollection))
                        .And(SpecificationProvider.IsNotValueTypeOfValueType.And(SpecificationProvider.IsNotStringOfString)),
                    new Handlers.OnTypeValueCollectionOfCollection<TClass, TComp>());

            return builder.Build();
        }
    }
}
