using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Astron.Expressions.Builder;
using Xunit;

namespace Astron.Expressions.Tests
{
    public class ImmutableFactory
    {
        private ImmutableDictionary<int, string> _dictionary;

        public int Count() => _dictionary.Count;

        internal ImmutableFactory(ImmutableDictionary<int, string> dictionary)
        {
            _dictionary = dictionary;
        }
    }

    public class ImmutableFactoryBuilder : FactoryBuilder<int, string, ImmutableFactory>
    {
        public int Count() => Builder.Count;

        public override ImmutableFactory Build()
            => CreateFactory(dict => new ImmutableFactory(dict));
    }

    public class FactoryBuilderTests
    {
        private ImmutableFactoryBuilder _builder;

        public FactoryBuilderTests()
        {
            _builder = new ImmutableFactoryBuilder();
        }

        [Fact] public void Build_ShouldThrow_OnDictEmpty()
            => Assert.Throws<ArgumentOutOfRangeException>(() => _builder.Build());

        [Fact]
        public void Register_ShouldAddToDict()
        {
            _builder.Register(1, "one");
            Assert.Equal(1, _builder.Count());
        }

        [Fact]
        public void Build_ShouldClearDict()
        {
            _builder.Register(1, "one");
            _builder.Build();
            Assert.Equal(0, _builder.Count());
        }

        [Fact]
        public void Build_ShouldReturnNewInstance()
        {
            _builder.Register(1, "one");
            var instance = _builder.Build();
            Assert.NotNull(instance);
            Assert.Equal(1, instance.Count());
        }
    }
}
