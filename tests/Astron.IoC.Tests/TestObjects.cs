using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.IoC.Tests
{
    internal class MappedType
    {
        public int Value { get; set; }
    }

    internal class SecondMappedType
    {
        public int Value { get; set; }
    }

    internal class ThirdMappedType { }

    internal class Simple { }
    internal class SecondSimple { }
    internal class ThirdSimple { }

    internal class Singleton
    {
        public int Value { get; set; }
    }

    internal class CustomInitialized { }

    internal class ValuesInitialized
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public ValuesInitialized(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }

    internal interface IDependency { }

    internal class Dependency : IDependency { }

    internal class DepInitialized
    {
        public IDependency Dependency { get; set; }

        public DepInitialized(IDependency dependency)
        {
            Dependency = dependency;
        }
    }

    internal class DepWithValuesInitialized
    {
        public IDependency Dependency { get; set; }
        public int Value { get; set; }

        public DepWithValuesInitialized(IDependency dependency, int value)
        {
            Dependency = dependency;
            Value = value;
        }
    }

    // bis because they are stored in static generic class
    // therefore they're shared between tests
    internal class SimpleBis { }
    internal class SecondSimpleBis { }
    internal class ThirdSimpleBis { }

    internal class SingletonBis
    {
        public int Value { get; set; }
    }

    internal class CustomInitializedBis { }

    internal class ValuesInitializedBis
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public ValuesInitializedBis(int value, string name)
        {
            Value = value;
            Name = name;
        }
    }

    internal interface IDependencyBis { }

    internal class DependencyBis : IDependencyBis { }

    internal class DepInitializedBis
    {
        public IDependencyBis Dependency { get; set; }

        public DepInitializedBis(IDependencyBis dependency)
        {
            Dependency = dependency;
        }
    }

    internal class DepWithValuesInitializedBis
    {
        public IDependencyBis Dependency { get; set; }
        public int Value { get; set; }

        public DepWithValuesInitializedBis(IDependencyBis dependency, int value)
        {
            Dependency = dependency;
            Value = value;
        }
    }
}
