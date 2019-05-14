using System;
using System.Linq.Expressions;
using System.Reflection;
using AgileObjects.ReadableExpressions;
using Astron.Binary.Reader;
using Astron.Expressions.Helpers;
using Astron.Expressions.Matching;
using Astron.Memory;

namespace Astron.Serialization.Deserialize.Expressions
{
    public class DesMethodBuilder<TClass, TComp> : IDesMethodBuilderOf<TClass, TComp>
        where TComp : IDesExprCompilerOf<TClass>, new()
    {
        private Expression<Action<IDeserializer, IReader, IMemoryPolicy, TClass>> _builtExpr;

        public IMatchingStrategy<PropertyInfo, TComp> Strategy { protected get; set; }
        protected TComp ExprCompiler { get; }
        protected bool IsAlreadyBuilt { get; private set; }

        protected void SetExpr()
        {
            _builtExpr = ExprCompiler.ToExpression();
            IsAlreadyBuilt = true;
        }

        public DesMethodBuilder() => ExprCompiler = new TComp();

        public Action<IDeserializer, IReader, IMemoryPolicy, TClass> Build()
        {
            if (IsAlreadyBuilt) return _builtExpr.Compile();

            CreateExpression();
            return ExprCompiler.Compile();
        }

        protected virtual void CreateExpression()
        {
            if (IsAlreadyBuilt) return;

            ExprCompiler.Parameter<IDeserializer>("deserializer");
            ExprCompiler.Parameter<IReader>("reader");
            ExprCompiler.Parameter<IMemoryPolicy>("policy");
            ExprCompiler.Parameter<TClass>("value");

            var properties = PropertyHelper.SortPropertiesOf<TClass>();
            foreach (var property in properties) Strategy.Process(property, ExprCompiler);

            SetExpr();
        }

        /// <summary>
        /// Call the ToString() method of the current expression compiler
        /// </summary>
        /// <returns></returns>
        public override string ToString() => IsAlreadyBuilt ? _builtExpr.ToReadableString() : ExprCompiler.ToString();
    }
}
