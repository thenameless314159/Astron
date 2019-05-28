using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Astron.Expressions.Helpers;

[assembly: InternalsVisibleTo("Astron.Expressions.Tests")]
namespace Astron.Expressions
{
    public class ExpressionCompiler<TDelegate> : IExpressionCompiler<TDelegate> where TDelegate : Delegate
    {
        private readonly List<Expression> _content = new List<Expression>();
        protected readonly List<Expression> Expressions = new List<Expression>();
        protected readonly List<ParameterExpression> Parameters = new List<ParameterExpression>();
        protected readonly List<ParameterExpression> Variables = new List<ParameterExpression>();

        public int Count() => _content.Count;

        /// <summary>
        /// Register an <see cref="Expression"/> to the general container
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to add</param>
        /// <returns>The index of the added <see cref="Expression"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int RegisterExpression(Expression expression)
        {
            Expressions.Add(expression);
            return Expressions.Count - 1;
        }

        /// <summary>
        /// Register an <see cref="Expression"/> to the content expression container
        /// </summary>
        /// <param name="expression">The <see cref="Expression"/> to add</param>
        /// <returns>The index of the added <see cref="Expression"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int EmitExpression(Expression expression)
        {
            _content.Add(expression);
            return Expressions.Count - 1;
        }

        protected Expression ToBlock()
        {
            if (!_content.Any())
                _content.Add(Expression.Empty());
            return Expression.Block(Variables, _content);
        }

        public int Parameter<T>(string name)
        {
            if (Expressions.OfType<ParameterExpression>().Any(e => e.Name == name && e.Type == typeof(T)))
                return GetParamIndex<T>(name);

            var exp = Expression.Parameter(typeof(T), name);
            Parameters.Add(exp);
            return RegisterExpression(exp);
        }

        public int Parameter(Type type, string name)
        {
            var exp = Expression.Parameter(type, name);
            Parameters.Add(exp);
            return RegisterExpression(exp);
        }

        public int Variable<T>(string name)
        {
            if (Expressions.OfType<ParameterExpression>().Any(e => e.Name == name && e.Type == typeof(T)))
                return GetParamIndex<T>(name);

            var expr = Expression.Variable(typeof(T), name);
            Variables.Add(expr);
            return RegisterExpression(expr);
        }

        public int Variable(Type type, string name)
        {
            var expr = Expression.Variable(type, name);
            Variables.Add(expr);
            return RegisterExpression(expr);
        }

        public int Constant(object value)
        {
            var expr = Expression.Constant(value);
            return RegisterExpression(expr);
        }

        public int Property(int paramIndex, PropertyInfo info)
        {
            var instance = Expressions[paramIndex];
            var expr = Expression.Property(instance, info);
            return RegisterExpression(expr);
        }

        public int StaticProperty(PropertyInfo info)
        {
            var expr = Expression.Property(null, info);
            return RegisterExpression(expr);
        }

        public int Field(int paramIndex, FieldInfo info)
        {
            var instance = Expressions[paramIndex];
            var expr = Expression.Field(instance, info);
            return RegisterExpression(expr);
        }

        public int StaticField(FieldInfo info)
        {
            var expr = Expression.Field(null, info);
            return RegisterExpression(expr);
        }

        public int GetParamIndex<T>(string name)
        {
            var existing =
                Expressions.OfType<ParameterExpression>()
                    .First(e => e.Name == name && e.Type == typeof(T));

            if (existing == null) throw new Exception("Parameter not found");
            return Expressions.IndexOf(existing);
        }

        public bool TryGetParamIndex<T>(string name, out int paramIndex)
        {
            paramIndex = 0;
            var existing =
                Expressions.OfType<ParameterExpression>()
                    .Any(e => e.Name == name && e.Type == typeof(T));

            if (!existing) return false;

            var value =
                Expressions.OfType<ParameterExpression>()
                    .First(e => e.Name == name && e.Type == typeof(T));
            paramIndex = Expressions.IndexOf(value);
            return true;
        }

        public int GetPropertyIndex(PropertyInfo info)
        {
            var existing =
                Expressions.OfType<MemberExpression>()
                    .First(m =>
                        m.Member.MemberType == MemberTypes.Property
                        && ((PropertyInfo)m.Member) == info);

            if (existing == null) throw new Exception("Parameter not found");
            return Expressions.IndexOf(existing);
        }

        public int GetFieldIndex(FieldInfo info)
        {
            var existing =
                Expressions.OfType<MemberExpression>()
                    .First(m =>
                        m.Member.MemberType == MemberTypes.Field
                        && ((FieldInfo)m.Member) == info);

            if (existing == null) throw new Exception("Parameter not found");
            return Expressions.IndexOf(existing);
        }

        public int Assign(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.Assign(leftExpr, rightExpr);
            return RegisterExpression(expr);
        }

        public int PostIncrementAssign(int paramIndex)
        {
            var paramExpr = Expressions[paramIndex];
            var expr = Expression.PostIncrementAssign(paramExpr);
            return RegisterExpression(expr);
        }

        public int PostDecrementAssign(int paramIndex)
        {
            var paramExpr = Expressions[paramIndex];
            var expr = Expression.PostDecrementAssign(paramExpr);
            return RegisterExpression(expr);
        }

        public int Convert<T>(int valueIndex)
        {
            var paramExpr = Expressions[valueIndex];
            var expr = Expression.Convert(paramExpr, typeof(T));
            return RegisterExpression(expr);
        }

        public int Convert(int valueIndex, Type toConvert)
        {
            var paramExpr = Expressions[valueIndex];
            var expr = Expression.Convert(paramExpr, toConvert);
            return RegisterExpression(expr);
        }

        public int CastOrUnbox(int valueIndex, Type toType)
        {
            var tempQualifier = Expressions[valueIndex];
            var cast = toType.GetTypeInfo().IsValueType
                ? Expression.Unbox(tempQualifier, toType)
                : Expression.Convert(tempQualifier, toType);
            var expr = (Expression)cast;
            return RegisterExpression(expr);
        }

        public int New<T>()
        {
            var expr = Expression.New(typeof(T));
            return RegisterExpression(expr);
        }

        public int New(Type type)
        {
            var expr = Expression.New(type);
            return RegisterExpression(expr);
        }

        public int New(ConstructorInfo info, params int[] argsIndexes)
        {
            var argsExpr = argsIndexes.Select(i => Expressions[i]);
            var expr = Expression.New(info, argsExpr);
            return RegisterExpression(expr);
        }

        public int Call(MethodInfo method, int targetIndex, params int[] argsIndexes)
        {
            var instance = Expressions[targetIndex];
            var argsExpr = argsIndexes.Select(i => Expressions[i]);

            var expr = Expression.Call(instance, method, argsExpr);
            return RegisterExpression(expr);
        }

        public int StaticCall(MethodInfo method, params int[] argsIndexes)
        {
            var argsExpr = argsIndexes.Select(i => Expressions[i]);

            var expr = Expression.Call(null, method, argsExpr);
            return RegisterExpression(expr);
        }

        public int ArrayAccess(int paramArrayIndex, params int[] toAccessIndexes)
        {
            var arrayExpr = Expressions[paramArrayIndex];
            var indexesExpr = toAccessIndexes.Select(i => Expressions[i]);
            var expr = Expression.ArrayAccess(arrayExpr, indexesExpr);
            return RegisterExpression(expr);
        }

        public int For(int loopVarIndex, int initValueIndex, int conditionIndex, int incrementIndex,
            params int[] loopContentIndexes)
        {
            var loopVarExpr = (ParameterExpression)Expressions[loopVarIndex];
            var initValueExpr = Expressions[initValueIndex];
            var conditionExpr = Expressions[conditionIndex];
            var incrementExpr = Expressions[incrementIndex];
            var loopContentBlock = Expression.Block(loopContentIndexes.Select(i => Expressions[i]));

            var expr =
                LoopProvider.For(loopVarExpr, initValueExpr, conditionExpr, incrementExpr, loopContentBlock);
            return RegisterExpression(expr);
        }

        public int For(int loopVarIndex, int maxIterationsIndex, params int[] loopContentIndexes)
        {
            var loopVarExpr = (ParameterExpression)Expressions[loopVarIndex];
            var maxItExpr = Expressions[maxIterationsIndex];
            var initValueExpr = Expression.Constant(0);
            var conditionExpr = Expression.LessThan(loopVarExpr, maxItExpr);
            var incrementExpr = Expression.PostIncrementAssign(loopVarExpr);
            var loopContentBlock = Expression.Block(loopContentIndexes.Select(i => Expressions[i]));

            var expr =
                LoopProvider.For(loopVarExpr, initValueExpr, conditionExpr, incrementExpr, loopContentBlock);
            return RegisterExpression(expr);
        }

        public int Foreach(int loopVarIndex, int collectionIndex, params int[] loopContentIndexes)
        {
            var loopVarExpr = (ParameterExpression)Expressions[loopVarIndex];
            var collectionExpr = Expressions[collectionIndex];
            var expr = LoopProvider.ForEach(collectionExpr, loopVarExpr,
                Expression.Block(loopContentIndexes.Select(i => Expressions[i])));

            return RegisterExpression(expr);
        }

        public int GreaterThan(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.GreaterThan(leftExpr, rightExpr);
            return RegisterExpression(expr);
        }

        public int LessThan(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.LessThan(leftExpr, rightExpr);
            return RegisterExpression(expr);
        }

        public IExpressionCompiler<TDelegate> EmitAssign(int leftIndex, int rightIndex)
        {
            var leftExpr = Expressions[leftIndex];
            var rightExpr = Expressions[rightIndex];

            var expr = Expression.Assign(leftExpr, rightExpr);
            EmitExpression(expr);
            return this;
        }

        public IExpressionCompiler<TDelegate> EmitCall(MethodInfo method, int targetIndex, params int[] argsIndexes)
        {
            var instance = Expressions[targetIndex];
            var argsExpr = argsIndexes.Select(i => Expressions[i]);

            var expr = Expression.Call(instance, method, argsExpr);
            EmitExpression(expr);
            return this;
        }

        public IExpressionCompiler<TDelegate> EmitStaticCall(MethodInfo method, params int[] argsIndexes)
        {
            var argsExpr = argsIndexes.Select(i => Expressions[i]);

            var expr = Expression.Call(null, method, argsExpr);
            EmitExpression(expr);
            return this;
        }

        public IExpressionCompiler<TDelegate> EmitReturn<T>(int valueIndex)
        {
            var value = Expressions[valueIndex];
            var returnTarget = Expression.Label(typeof(T));
            var returnLabel =
                Expression.Label(
                    returnTarget,
                    typeof(T).IsValueType ? (Expression)Expression.Constant(default(T)) : Expression.New(typeof(T)));
            var returnExpression = Expression.Return(returnTarget, value, typeof(int));

            EmitExpression(returnExpression);
            EmitExpression(returnLabel);
            return this;
        }

        public IExpressionCompiler<TDelegate> Emit(int expressionIndex)
        {
            var expr = Expressions[expressionIndex];
            EmitExpression(expr);
            return this;
        }

        public TDelegate Compile() => ToExpression().Compile();
        public Expression<TDelegate> ToExpression() => Expression.Lambda<TDelegate>(ToBlock(), Parameters);
    }
}
