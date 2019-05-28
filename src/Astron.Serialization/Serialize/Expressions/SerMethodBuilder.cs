using System;
using System.Linq.Expressions;
using System.Reflection;
using Astron.Binary.Writer;
using Astron.Expressions.Helpers;
using Astron.Expressions.Matching;

namespace Astron.Serialization.Serialize.Expressions
{
    public class SerMethodBuilder<TClass, TComp> : ISerMethodBuilderOf<TClass, TComp>
        where TComp : ISerExprCompilerOf<TClass>, new()
    {
        private Expression<Action<ISerializer, IWriter, TClass>> _builtExpr;

        public IMatchingStrategy<PropertyInfo, TComp> Strategy { protected get; set; }
        protected TComp ExprCompiler { get; }
        protected bool IsAlreadyBuilt { get; private set; }

        protected void SetExpr()
        {
            _builtExpr = ExprCompiler.ToExpression();
            IsAlreadyBuilt = true;
        }

        public SerMethodBuilder() => ExprCompiler = new TComp();

        public Action<ISerializer, IWriter, TClass> Build()
        {
            if (IsAlreadyBuilt) return _builtExpr.Compile();

            CreateExpression();
            return ExprCompiler.Compile();
        }

        protected virtual void CreateExpression()
        {
            ExprCompiler.Parameter<ISerializer>("serializer");
            ExprCompiler.Parameter<IWriter>("writer");
            ExprCompiler.Parameter<TClass>("value");

            var properties = PropertyHelper.SortPropertiesOf<TClass>();
            foreach (var property in properties) Strategy.Process(property, ExprCompiler);

            SetExpr();
        }
    }
}
