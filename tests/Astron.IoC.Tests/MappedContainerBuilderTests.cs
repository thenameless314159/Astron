using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    internal class SubMappedContainerBuilder : MappedContainerBuilder
    {
        public int TypesCount => RegTypesBuilder.Immutable.Count;
        public int AnonCount => RegAnonymousBuilder.Immutable.Count;
    }
    public class MappedContainerBuilderTests
    {
        private readonly SubMappedContainerBuilder _builder = new SubMappedContainerBuilder();

        [Fact]
        public void Immutable_ShouldBuildOnce()
        {
            _builder.Register<MappedType>(1);
            _builder.Build();
            _builder.Register<FourthMappedType>(4);
            _builder.Build();

            Assert.Equal(1, _builder.TypesCount);
            Assert.Equal(1, _builder.AnonCount);
        }

        [Fact]
        public void RegisterT_ShouldRegister()
        {
            _builder.Register<SecondMappedType>(1);
            _builder.Build();

            Assert.Equal(1, _builder.TypesCount);
            Assert.Equal(1, _builder.AnonCount);
        }

        [Fact]
        public void Build_ShouldCreateInstance()
            => Assert.NotNull(_builder.Build());
    }
}
