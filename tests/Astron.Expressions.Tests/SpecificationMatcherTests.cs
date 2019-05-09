using System.Collections.Generic;
using System.Collections.Immutable;
using Astron.Expressions.Matching;
using Astron.Expressions.Specifications;
using Xunit;

namespace Astron.Expressions.Tests
{
    public class Dependency
    {
        public string Value { get; set; }
    }

    public class SpecHandler1 : IMatchingHandler<int, List<string>>
    {
        public void OnMatch(int input, List<string> dependency)
        {
            dependency.Add("zero");
        }
    }

    public class SpecHandler2 : IMatchingHandler<int, List<string>>
    {
        public void OnMatch(int input, List<string> dependency)
        {
            dependency.Add("two");
        }
    }

    public class SpecHandler3 : IMatchingHandler<int, List<string>>
    {
        public void OnMatch(int input, List<string> dependency)
        {
            dependency.Add("three");
        }
    }


    public class SpecificationMatcherTests
    {
        private IMatchingStrategy<int, List<string>> _firstMatchStrategy;
        private IMatchingStrategy<int, List<string>> _allMatchStrategy;

        public SpecificationMatcherTests()
        {
            var equalZero = new Specification<int>(v => v == 0);
            var equalTwo = new Specification<int>(v => v == 2);
            var greaterThanOne = new Specification<int>(v => v > 1);

            var builder = ImmutableDictionary.CreateBuilder<ISpecification<int>, IMatchingHandler<int, List<string>>>();
            builder.Add(equalZero, new SpecHandler1());
            builder.Add(equalTwo, new SpecHandler2());
            builder.Add(greaterThanOne, new SpecHandler3());

            _firstMatchStrategy = new SpecificationMatcher<int, List<string>>(builder.ToImmutable(), true);
            _allMatchStrategy = new SpecificationMatcher<int, List<string>>(builder.ToImmutable());
        }

        [Fact]
        public void Process_ShouldExecuteFirstMatch()
        {
            var dep  = new List<string>();
            _firstMatchStrategy.Process(0, dep);
            Assert.Single(dep);
            Assert.Equal("zero", dep[0]);
        }

        [Fact]
        public void Process_ShouldExecuteAllMatch()
        { 
            var dep = new List<string>();
            _allMatchStrategy.Process(2, dep);
            Assert.Equal(2, dep.Count);
        }
    }
}
