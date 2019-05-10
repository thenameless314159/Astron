using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    internal class SubMappedContainerOfBuilder : MappedContainerBuilder<MappedType>
    {
        public int Count => MappedInstancesBuilder.Immutable.Count;
    }

    public class MappedContainerOfBuilderTests
    {
        private readonly SubMappedContainerOfBuilder _builder = new SubMappedContainerOfBuilder();


        [Fact]
        public void Immutable_ShouldBuildOnce()
        {
            _builder.Register(1, new MappedType());
            _builder.Build();
            _builder.Register(2, new MappedType());
            _builder.Build();

            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_ShouldRegister()
        {
            _builder.Register(1, new MappedType());
            _builder.Build();

            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void Build_ShouldCreateInstance()
            => Assert.NotNull(_builder.Build());
    }
}
