using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Astron.Expressions.Helpers;
using Xunit;

namespace Astron.Expressions.Tests
{
    internal class Main
    {
        public short S { get; set; }
        public int I { get; set; }
    }

    internal class Second : Main
    {
        public long L { get; set; }
    }
    public class PropertyHelperTests
    {
        [Fact]
        public void GetProperties_ShouldBeUnordered()
        {
            var firtProp = typeof(Second).GetProperties().First();
            var realFirstProp = typeof(Second).GetProperty("S");
            Assert.NotEqual(realFirstProp, firtProp);
        }

        [Fact]
        public void SortedProperties_ShouldBeOrdered()
        {
            var sortedProperties = PropertyHelper.SortPropertiesOf<Second>();
            var realFirstProp = typeof(Second).GetProperty("S");
            Assert.NotEqual(realFirstProp, sortedProperties[0]);
        }
    }
}
