using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    public class MappedContainerOfTests
    {
        private static readonly IMappedContainer<MappedType> Container = new MappedContainerBuilder<MappedType>()
            .Register(1, new MappedType { Value = 1 })
            .Register(2, new MappedType { Value = 2 })
            .Register(3, new MappedType { Value = 3 })
            .Build();

        [Fact]
        public void GetInstanceT_ShouldReturnCorrectInstance()
        {
            var instance = Container.GetInstance(1);
            Assert.NotNull(instance);
            Assert.Equal(1, instance.Value);
        }

        [Fact]
        public void TryGetInstanceT_ShouldReturnTrue_WhenRegistered()
        {
            var isGet = Container.TryGetInstance(3, out var instance);
            Assert.True(isGet);
            Assert.NotNull(instance);
            Assert.Equal(3, instance.Value);
        }

        [Fact]
        public void TryGetInstanceT_ShouldReturnFalse_WhenNotRegistered()
        {
            var isGet = Container.TryGetInstance(4, out var instance);
            Assert.False(isGet);
            Assert.Null(instance);
        }
    }
}
