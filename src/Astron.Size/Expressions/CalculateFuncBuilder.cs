using System;
using System.Linq.Expressions;
using System.Reflection;
using AgileObjects.ReadableExpressions;
using Astron.Expressions.Helpers;
using Astron.Expressions.Matching;

namespace Astron.Size.Expressions
{
    public class CalculateFuncBuilder<TClass, TComp> : ICalculateFuncBuilderOf<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>, new()
    {
        private Expression<Func<ISizing, TClass, int>> _builtExpr;

        

        public IMatchingStrategy<PropertyInfo, TComp> Strategy { protected get; set; }

        public CalculateFuncBuilder() => ExprCompiler = new TComp();

        protected TComp ExprCompiler { get; }
        protected bool IsAlreadyBuilt { get; private set; }

        protected void SetExpr()
        {
            _builtExpr = ExprCompiler.ToExpression();
            IsAlreadyBuilt = true;
        }

        protected virtual void CreateExpression()
        {
            if (IsAlreadyBuilt) return;

            ExprCompiler.Parameter<ISizing>("sizing");
            ExprCompiler.Parameter<TClass>("value");
            var size = ExprCompiler.Variable<int>("size");

            var properties = PropertyHelper.SortPropertiesOf<TClass>();
            foreach (var property in properties) Strategy.Process(property, ExprCompiler);

            ExprCompiler.EmitReturn<int>(size);
            SetExpr();
        }

        public Func<ISizing, TClass, int> Build()
        {
            if (IsAlreadyBuilt) return _builtExpr.Compile();

            CreateExpression();
            return ExprCompiler.Compile();
        }

        /// <summary>
        /// Call the ToString() method of the current expression compiler
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IsAlreadyBuilt ? _builtExpr.ToReadableString() : ExprCompiler.ToString();
    }
}
