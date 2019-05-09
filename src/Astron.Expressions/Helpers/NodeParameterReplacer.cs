using System.Linq.Expressions;

namespace Astron.Expressions.Helpers
{
    public class NodeParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
            => base.VisitParameter(_parameter);

        public NodeParameterReplacer(ParameterExpression parameter)
            => _parameter = parameter;
    }
}
