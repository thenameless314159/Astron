using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Astron.Expressions;

namespace Astron.Size.Expressions
{
    public class CalculateFuncCompilerOf<TClass> : ExpressionCompiler<Func<ISizing, TClass, int>>, ICalculateFuncCompilerOf<TClass>
    {
        private static readonly MethodInfo SizeOfMethod =
            typeof(ISizing).GetMethods().First(m => !m.GetParameters().Any());

        private static readonly MethodInfo SizeOfInstanceMethod =
            typeof(ISizing).GetMethods().First(m => m.GetParameters().Any());

        public int AddAssign(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.AddAssign(leftExpr, rightExpr);
            return RegisterExpression(expr);
        }

        public int Multiply(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.Multiply(leftExpr, rightExpr);
            return RegisterExpression(expr);
        }

        public int CallSizeOf(Type type, int sizingIndex)
            => Call(SizeOfMethod.MakeGenericMethod(type), sizingIndex);

        public int CallSizeOf(Type type, int sizingIndex, int sizedIndex)
            => Call(SizeOfInstanceMethod.MakeGenericMethod(type), sizingIndex, sizedIndex);
    }
}
