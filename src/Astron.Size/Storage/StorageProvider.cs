using System;
using System.Collections.Generic;
using System.Text;
using Astron.Expressions.Helpers;
using Astron.Size.Cache;
using Astron.Size.Expressions;
using Astron.Size.Matching;

namespace Astron.Size.Storage
{
    public static class StorageProvider
    {
        public static ISizeOfStorage<T> CreateLazy<T>()
        {
            LazySizeOfCache<T>.Builder = () => CreateCompiled<T>();
            return new SizeOfStorage<T>((s, v) => LazySizeOfCache<T>.Calculate(s, v));
        }

        public static ISizeOfStorage<T> CreateLazy<T, TComp, TBuilder, TProvider>()
            where TComp : ICalculateFuncCompilerOf<T>
            where TBuilder : ICalculateFuncBuilderOf<T, TComp>, new()
            where TProvider : ISizeMatchingProviderOf<T, TComp>, new()
        {
            LazySizeOfCache<T>.Builder = () => CreateCompiled<T, TComp, TBuilder, TProvider>();
            return new SizeOfStorage<T>((s, v) => LazySizeOfCache<T>.Calculate(s, v));
        }

        public static ISizeOfStorage<T> CreateCompiled<T>()
            => CreateCompiled<T, 
                CalculateFuncCompilerOf<T>,
                CalculateFuncBuilder<T, CalculateFuncCompilerOf<T>>,
                SizeMatchingProviderOf<T, CalculateFuncCompilerOf<T>>>();


        public static ISizeOfStorage<T> CreateCompiled<T, TComp, TBuilder, TProvider>()
            where TComp : ICalculateFuncCompilerOf<T>
            where TBuilder : ICalculateFuncBuilderOf<T, TComp>, new()
            where TProvider : ISizeMatchingProviderOf<T, TComp>, new()
        {
            var provider = new TProvider();
            var builder = new TBuilder();

            var strategy = provider.Build();
            builder.Strategy = strategy;
            return new SizeOfStorage<T>(builder.Build());
        }

        public static unsafe ISizeStorage<T> CreatePrimitive<T>() where T : unmanaged
        {
            if (!PrimitiveTypes.Primitives.Contains(typeof(T)))
                throw new ArgumentOutOfRangeException($"{typeof(T).Name} is not a primitive type !");

            return new SizeValueStorage<T>(sizeof(T));
        }
    }
}
