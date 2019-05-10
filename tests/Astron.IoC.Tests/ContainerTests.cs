using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Astron.IoC.Tests
{
    public class ContainerTests
    {
        private static readonly IContainer Container = new ContainerBuilder()
            .Register<Simple>()
            .Register(new Singleton { Value = 5 })
            .Register(new Dependency() as IDependency)
            .Register(ctr => new DepInitialized(ctr.GetInstance<IDependency>()))
            .Register(args => new ValuesInitialized((int)args[0], (string)args[1]))
            .Register((ctr, args) => new DepWithValuesInitialized(ctr.GetInstance<IDependency>(), (int)args[0]))
            .Build();

        [Fact]
        public void GetInstanceT_ShouldReturnNew()
        {
            var instance1 = Container.GetInstance<Simple>();
            var instance2 = Container.GetInstance<Simple>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotEqual(instance1, instance2);
        }

        [Fact]
        public void GetInstanceT_ShouldReturnSingleton()
        {
            var instance1 = Container.GetInstance<Singleton>();
            var instance2 = Container.GetInstance<Singleton>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.Equal(instance1, instance2);
            Assert.Equal(instance1.Value, instance2.Value);
        }

        [Fact]
        public void GetInstanceT_ShouldReturnNew_WithDependency()
        {
            var instance1 = Container.GetInstance<DepInitialized>();
            var instance2 = Container.GetInstance<DepInitialized>();

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance1.Dependency);
            Assert.NotNull(instance2.Dependency);
            Assert.NotEqual(instance1, instance2);
            Assert.Equal(instance1.Dependency, instance2.Dependency);
        }

        [Theory,
        InlineData("1", "propertyOne"),
        InlineData("2", "propertyTwo"),
        InlineData("3", "propertyThree")]
        public void GetInstanceT_ShouldReturnNew_WithValues(string id, string name)
        {
            var realId = int.Parse(id);
            var instance = Container.GetInstance<ValuesInitialized>(realId, name);

            Assert.NotNull(instance);
            Assert.Equal(realId, instance.Value);
            Assert.Equal(name, instance.Name);
        }

        [Theory,
        InlineData(1),
        InlineData(10),
        InlineData(100)]
        public void GetInstanceT_ShouldReturnNew_WithDependencyAndValues(int value)
        {
            var instance1 = Container.GetInstance<DepWithValuesInitialized>(value);
            var instance2 = Container.GetInstance<DepWithValuesInitialized>(value);

            Assert.NotNull(instance1);
            Assert.NotNull(instance2);
            Assert.NotNull(instance1.Dependency);
            Assert.NotNull(instance2.Dependency);
            Assert.Equal(value, instance1.Value);
            Assert.Equal(value, instance2.Value);
            Assert.Equal(instance1.Dependency, instance2.Dependency);
            
        }
    }
}
