using System;
using System.Collections.Generic;
using System.Text;
using Astron.Expressions.Helpers;
using Astron.Expressions.Specifications;
using Xunit;

namespace Astron.Expressions.Tests
{
    public class SpecificationTests
    {
        [Theory,
         InlineData(1),
         InlineData(10),
         InlineData(100)]
        public void IsSatisfiedBy_ShouldBeTrueWith(int value)
        {
            var specification = new Specification<int>(v => v > 0);
            Assert.True(specification.IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(1 * -1),
         InlineData(10 * -1),
         InlineData(100 * -1)]
        public void IsSatisfiedBy_ShouldBeFalseWith(int value)
        {
            var specification = new Specification<int>(v => v > 0);
            Assert.False(specification.IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(1),
         InlineData(10),
         InlineData(100)]
        public void And_IsSatisfiedBy_ShouldBeTrueWith(int value)
        {
            var specification = new Specification<int>(v => v > 0);
            var secondSpec = new Specification<int>(v => v < 101);
            Assert.True(specification.And(secondSpec).IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(10),
         InlineData(100),
         InlineData(1000)]
        public void And_IsSatisfiedBy_ShouldBeFalseWith(int value)
        {
            var specification = new Specification<int>(v => v > 0);
            var secondSpec = new Specification<int>(v => v < 10);
            Assert.False(specification.And(secondSpec).IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(1),
         InlineData(10),
         InlineData(100)]
        public void Or_IsSatisfiedBy_ShouldBeTrueWith(int value)
        {
            var specification = new Specification<int>(v => v == 1);
            var secondSpec = new Specification<int>(v => v == 10);
            var thirdSpec = new Specification<int>(v => v == 100);
            var fourthSpec = new Specification<int>(v => v < 0);

            Assert.True(
                specification
                    .Or(secondSpec)
                    .Or(thirdSpec)
                    .Or(fourthSpec)
                    .IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(10),
         InlineData(100),
         InlineData(1000)]
        public void Or_IsSatisfiedBy_ShouldBeFalseWith(int value)
        {
            var specification = new Specification<int>(v => v == 1);
            var secondSpec = new Specification<int>(v => v < 10);
            Assert.False(specification.Or(secondSpec).IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(10),
         InlineData(100),
         InlineData(1000)]
        public void Not_IsSatisfiedBy_ShouldBeTrueWith(int value)
        {
            var specification = new Specification<int>(v => v < 1);
            Assert.True(specification.Not().IsSatisfiedBy(value));
        }

        [Theory,
         InlineData(10),
         InlineData(100),
         InlineData(1000)]
        public void Not_IsSatisfiedBy_ShouldBeFalseWith(int value)
        {
            var specification = new Specification<int>(v => v > 1);
            Assert.False(specification.Not().IsSatisfiedBy(value));
        }
    }
}
