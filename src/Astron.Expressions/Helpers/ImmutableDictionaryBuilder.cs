using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Astron.Expressions.Helpers
{
    public class ImmutableDictionaryBuilder<TKey, TValue>
    {
        private readonly ImmutableDictionary<TKey, TValue>.Builder _builder =
            ImmutableDictionary.CreateBuilder<TKey, TValue>();

        public ImmutableDictionary<TKey, TValue> Immutable { get; private set; }

        public TValue this[TKey key]
        {
            set => _builder[key] = value;
        }

        public int Count() => _builder.Count;
        public bool ContainsKey(TKey key) => _builder.ContainsKey(key);
        public bool ContainsValue(TValue value) => _builder.ContainsValue(value);

        /// <summary>
        /// Builds and clear the current dictionary.
        /// </summary>
        public void Build()
        {
            Immutable = _builder.ToImmutable();
            _builder.Clear();
        }
    }
}
