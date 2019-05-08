using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Astron.Expressions.Helpers;
using Xunit;

namespace Astron.Expressions.Tests
{
    public class NodeParameterReplacerTests
    {
        [Fact]
        public void Visit_AddFive_ShouldReplaceParameter()
        {
            var paramInput = Expression.Parameter(typeof(int), "input");
            var newInput = Expression.Parameter(typeof(int), "newInput");

            var addFiveExpr = Expression.Add(paramInput, Expression.Constant(5));
            var newAddFiveExpr = (BinaryExpression)new NodeParameterReplacer(newInput).Visit(addFiveExpr);

            var addFiveLeft = newAddFiveExpr?.Left as ParameterExpression;
            Assert.Equal(newInput, addFiveLeft);
        }
    }
}
