using Astron.Serialization.Cache;
using Astron.Serialization.Deserialize.Expressions;
using Astron.Serialization.Deserialize.Matching;

namespace Astron.Serialization.Storage
{
    public static class DeserializerStorageProvider
    {
        public static IDeserializerStorage<T> CreateLazy<T>()
        {
            LazyDeserializeMethodCache<T>.Builder = () => CreateCompiled<T>();
            return new DeserializerStorage<T>((d, r, p, v) => LazyDeserializeMethodCache<T>.Deserialize(d, r, p, v));
        }

        public static IDeserializerStorage<T> CreateLazy<T, TProvider>()
            where TProvider : IDesMatchingProviderOf<T, DesExprCompilerOf<T>>, new()
        {
            LazyDeserializeMethodCache<T>.Builder = () => CreateCompiled<T, TProvider>();
            return new DeserializerStorage<T>((d, r, p, v) => LazyDeserializeMethodCache<T>.Deserialize(d, r, p, v));
        }

        public static IDeserializerStorage<T> CreateLazy<T, TBuilder, TProvider>()
            where TBuilder : IDesMethodBuilderOf<T, DesExprCompilerOf<T>>, new()
            where TProvider : IDesMatchingProviderOf<T, DesExprCompilerOf<T>>, new()
        {
            LazyDeserializeMethodCache<T>.Builder = () => CreateCompiled<T, TBuilder, TProvider>();
            return new DeserializerStorage<T>((d, r, p, v) => LazyDeserializeMethodCache<T>.Deserialize(d, r, p, v));
        }

        public static IDeserializerStorage<T> CreateLazy<T, TComp, TBuilder, TProvider>()
            where TComp : IDesExprCompilerOf<T>
            where TBuilder : IDesMethodBuilderOf<T, TComp>, new()
            where TProvider : IDesMatchingProviderOf<T, TComp>, new()
        {
            LazyDeserializeMethodCache<T>.Builder = () => CreateCompiled<T, TComp, TBuilder, TProvider>();
            return new DeserializerStorage<T>((d, r, p, v) => LazyDeserializeMethodCache<T>.Deserialize(d, r, p, v));
        }

        public static IDeserializerStorage<T> CreateCompiled<T>()
            => CreateCompiled<T, DesMatchingProviderOf<T, DesExprCompilerOf<T>>>();

        public static IDeserializerStorage<T> CreateCompiled<T, TProvider>()
            where TProvider : IDesMatchingProviderOf<T, DesExprCompilerOf<T>>, new()
            => CreateCompiled<T, DesMethodBuilder<T, DesExprCompilerOf<T>>, TProvider>();

        public static IDeserializerStorage<T> CreateCompiled<T, TBuilder, TProvider>()
            where TBuilder : IDesMethodBuilderOf<T, DesExprCompilerOf<T>>, new()
            where TProvider : IDesMatchingProviderOf<T, DesExprCompilerOf<T>>, new()
            => CreateCompiled<T, DesExprCompilerOf<T>, TBuilder, TProvider>();

        public static IDeserializerStorage<T> CreateCompiled<T, TComp, TBuilder, TProvider>()
            where TComp : IDesExprCompilerOf<T>
            where TBuilder : IDesMethodBuilderOf<T, TComp>, new()
            where TProvider : IDesMatchingProviderOf<T, TComp>, new()
        {
            var provider = new TProvider();
            var builder = new TBuilder();

            var strategy = provider.Build();
            builder.Strategy = strategy;
            return new DeserializerStorage<T>(builder.Build());
        }
    }
}
