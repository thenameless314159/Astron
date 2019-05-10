using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    internal class SubContainerBuilder : ContainerBuilder
    {
        public int Count => RegisteredTypesBuilder.Immutable.Count;
    }
    public class ContainerBuilderTests
    {
        private SubContainerBuilder _builder = new SubContainerBuilder();

        [Fact]
        public void Immutable_ShouldBuildOnce()
        {
            _builder.Register<SimpleBis>();
            _builder.Build();
            _builder.Register<SecondSimpleBis>();
            _builder.Build();

            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_ShouldRegisterInitializer()
        {
            _builder.Register<ThirdSimpleBis>();
            _builder.Build();
            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_WithInstance_ShouldRegister()
        {
            _builder.Register(new SimpleBis());
            _builder.Build();
            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_WithInitializer_ShouldRegister()
        {
            _builder.Register(() => new CustomInitializedBis());
            _builder.Build();
            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_WithDepInitializer_ShouldRegister()
        {
            _builder
                .Register(new DependencyBis() as IDependencyBis)
                .Register(ctr => new DepInitializedBis(ctr.GetInstance<IDependencyBis>()));

            _builder.Build();
            Assert.Equal(2, _builder.Count);
        }

        [Fact]
        public void RegisterT_WithValuesInitializer_ShouldRegister()
        {
            _builder.Register(args => new ValuesInitializedBis((int)args[0], (string)args[1]));
            _builder.Build();
            Assert.Equal(1, _builder.Count);
        }

        [Fact]
        public void RegisterT_WithDepAndValuesInitializer_ShouldRegister()
        {
            _builder.Register((ctr, args) => new DepWithValuesInitializedBis(
                ctr.GetInstance<IDependencyBis>(), 
                (int)args[0]));

            _builder.Build();
            Assert.Equal(1, _builder.Count);
        }
    }
}
