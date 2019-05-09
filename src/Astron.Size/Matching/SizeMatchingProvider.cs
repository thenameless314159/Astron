using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Builder;
using Astron.Expressions.Matching;
using Astron.Size.Expressions;
using Astron.Size.Matching.Handlers;
using static Astron.Expressions.Helpers.SpecificationProvider;

namespace Astron.Size.Matching
{
    public class SizeMatchingProviderOf<TClass, TComp> : ISizeMatchingProviderOf<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        public IMatchingStrategy<PropertyInfo, TComp> Build()
        {
            var builder = new SizeStrategyBuilder<TClass, TComp>();

            builder
                .Register(IsPrimitiveValue, new OnPrimitive<TClass, TComp>())
                .Register(IsNotValueTypeValue, new OnTypeValue<TClass, TComp>())
                .Register(IsPrimitiveArray, new OnPrimitiveArray<TClass, TComp>())
                .Register(IsNotValueTypeArray, new OnTypeValueArray<TClass, TComp>());

            return builder.Build();
        }
    }
}
