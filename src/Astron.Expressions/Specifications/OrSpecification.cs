using System;
using System.Linq.Expressions;
using Astron.Expressions.Helpers;

namespace Astron.Expressions.Specifications
{
    public class OrSpecification<T> : BaseSpecification<T>
    {
        private BaseSpecification<T> _left;
        private BaseSpecification<T> _right;

        public OrSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpr = _left.ToExpression();
            var rightExpr = _right.ToExpression();

            var paramT = Expression.Parameter(typeof(T));
            var paramReplacer = new NodeParameterReplacer(paramT);
            var exprBody = Expression.OrElse(leftExpr.Body, rightExpr.Body);

            exprBody = (BinaryExpression)paramReplacer.Visit(exprBody);
            return Expression.Lambda<Func<T, bool>>(exprBody, paramT);
        }
    }
}
