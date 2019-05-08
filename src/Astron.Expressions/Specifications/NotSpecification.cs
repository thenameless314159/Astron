using System;
using System.Linq.Expressions;
using Astron.Expressions.Helpers;

namespace Astron.Expressions.Specifications
{
    public class NotSpecification<T> : BaseSpecification<T>
    {
        private BaseSpecification<T> _main;

        public NotSpecification(BaseSpecification<T> main) => _main = main;

        public override Expression<Func<T, bool>> ToExpression()
        {
            var mainExpr = _main.ToExpression();
            var paramT = Expression.Parameter(typeof(T));
            var paramReplacer = new NodeParameterReplacer(paramT);
            var exprBody = Expression.Not(mainExpr.Body);

            exprBody = (UnaryExpression)paramReplacer.Visit(exprBody);
            return Expression.Lambda<Func<T, bool>>(exprBody, paramT);
        }
    }
}
