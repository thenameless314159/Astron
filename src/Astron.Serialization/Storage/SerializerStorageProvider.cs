using Astron.Serialization.Cache;
using Astron.Serialization.Serialize.Expressions;
using Astron.Serialization.Serialize.Matching;

namespace Astron.Serialization.Storage
{
    public static class SerializerStorageProvider
    {
        public static ISerializerStorage<T> CreateLazy<T>()
        {
            LazySerializeMethodCache<T>.Builder = () => CreateCompiled<T>();
            return new SerializerStorage<T>((s, w, v) => LazySerializeMethodCache<T>.Serialize(s, w, v));
        }

        public static ISerializerStorage<T> CreateLazy<T, TProvider>()
            where TProvider : ISerMatchingProviderOf<T, SerExprCompilerOf<T>>, new()
        {
            LazySerializeMethodCache<T>.Builder = () => CreateCompiled<T, TProvider>();
            return new SerializerStorage<T>((s, w, v) => LazySerializeMethodCache<T>.Serialize(s, w, v));
        }

        public static ISerializerStorage<T> CreateLazy<T, TBuilder, TProvider>()
            where TBuilder : ISerMethodBuilderOf<T, SerExprCompilerOf<T>>, new()
            where TProvider : ISerMatchingProviderOf<T, SerExprCompilerOf<T>>, new()
        {
            LazySerializeMethodCache<T>.Builder = () => CreateCompiled<T, TBuilder, TProvider>();
            return new SerializerStorage<T>((s, w, v) => LazySerializeMethodCache<T>.Serialize(s, w, v));
        }

        public static ISerializerStorage<T> CreateLazy<T, TComp, TBuilder, TProvider>()
            where TComp : ISerExprCompilerOf<T>
            where TBuilder : ISerMethodBuilderOf<T, TComp>, new()
            where TProvider : ISerMatchingProviderOf<T, TComp>, new()
        {
            LazySerializeMethodCache<T>.Builder = () => CreateCompiled<T, TComp, TBuilder, TProvider>();
            return new SerializerStorage<T>((s, w, v) => LazySerializeMethodCache<T>.Serialize(s, w, v));
        }

        public static ISerializerStorage<T> CreateCompiled<T>()
            => CreateCompiled<T, SerMatchingProviderOf<T, SerExprCompilerOf<T>>>();

        public static ISerializerStorage<T> CreateCompiled<T, TProvider>()
            where TProvider : ISerMatchingProviderOf<T, SerExprCompilerOf<T>>, new()
            => CreateCompiled<T, SerMethodBuilder<T, SerExprCompilerOf<T>>, TProvider>();

        public static ISerializerStorage<T> CreateCompiled<T, TBuilder, TProvider>()
            where TBuilder : ISerMethodBuilderOf<T, SerExprCompilerOf<T>>, new()
            where TProvider : ISerMatchingProviderOf<T, SerExprCompilerOf<T>>, new()
            => CreateCompiled<T, SerExprCompilerOf<T>, TBuilder, TProvider>();

        

        public static ISerializerStorage<T> CreateCompiled<T, TComp, TBuilder, TProvider>()
            where TComp : ISerExprCompilerOf<T>
            where TBuilder : ISerMethodBuilderOf<T, TComp>, new()
            where TProvider : ISerMatchingProviderOf<T, TComp>, new()
        {
            var provider = new TProvider();
            var builder = new TBuilder();

            var strategy = provider.Build();
            builder.Strategy = strategy;
            return new SerializerStorage<T>(builder.Build());
        }
    }
}
