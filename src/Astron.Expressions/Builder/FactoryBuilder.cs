using System;
using System.Collections.Immutable;

namespace Astron.Expressions.Builder
{
    public abstract class FactoryBuilder<TKey, TVal, TFact> : IFactoryBuilder<TKey, TVal, TFact>
    {
        protected readonly ImmutableDictionary<TKey, TVal>.Builder Builder;

        public abstract TFact Build();

        protected FactoryBuilder()
        {
            Builder = ImmutableDictionary.CreateBuilder<TKey, TVal>();
        }

        public IFactoryBuilder<TKey, TVal, TFact> Register(TKey key, TVal val)
        {
            Builder[key] = val;
            return this;
        }

        /// <summary>
        /// Builds a factory and clear the dictionary builder.
        /// </summary>
        /// <param name="builder">the func that build the factory</param>
        /// <returns>A <see cref="TFact"/> instance</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected TFact CreateFactory(Func<ImmutableDictionary<TKey, TVal>, TFact> builder)
        {
            if (Builder.Count < 1) throw new ArgumentOutOfRangeException(nameof(Builder.Count));
            var immutable = Builder.ToImmutable();
            Builder.Clear();
            return builder(immutable);
        }
    }
}
