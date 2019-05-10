using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    public class MappedContainerTests
    {
        private static readonly IMappedContainer Container = new MappedContainerBuilder()
            .Register<MappedType>(1)
            .Register<SecondMappedType>(2)
            .Register<ThirdMappedType>(3)
            .Build();

        [Fact]
        public void GetInstanceT_ShouldReturnNew()
            => Assert.NotNull(Container.GetInstance<MappedType>());

        [Fact]
        public void GetIdT_ShouldReturnCorrectValue()
            => Assert.Equal(3, Container.GetId<ThirdMappedType>());

        [Fact]
        public void IsEmptyT_ShouldReturnCorrectValue()
            => Assert.False(Container.IsEmpty<SecondMappedType>());

        [Fact]
        public void GetInstanceT_FromId_ShouldReturnNew()
            => Assert.NotNull(Container.GetInstance<MappedType>(1));

        [Fact]
        public void IsEmptyT_FromInstance_ShouldReturnCorrectValue()
            => Assert.False(Container.IsEmpty(new MappedType()));

        [Fact]
        public void GetId_FromInstance_ShouldReturnCorrectValue()
            => Assert.Equal(1, Container.GetId((object)new MappedType()));

        [Fact]
        public void IsEmpty_FromId_ShouldReturnCorrectValue()
            => Assert.True(Container.IsEmpty(3));

        [Fact]
        public void GetInstance_FromId_ShouldReturnNew()
        {
            var instance = Container.GetInstance(2);
            Assert.NotNull(instance);
            Assert.IsType<SecondMappedType>(instance);
        }
    }
}
