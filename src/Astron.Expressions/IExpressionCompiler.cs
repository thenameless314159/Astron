using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Astron.Expressions
{
    public interface IExpressionCompiler<TDelegate> where TDelegate : Delegate
    {
        #region Members
        /// <summary>
        /// Register a parameter <see cref="ParameterExpression"/> in the compiler
        /// </summary>
        /// <typeparam name="T">The type of the parameter</typeparam>
        /// <param name="name">The name of the parameter</param>
        /// <returns>The index of the <see cref="ParameterExpression"/> registered in the general container</returns>
        int Parameter<T>(string name);
        int Parameter(Type type, string name);

        /// <summary>
        /// Register a variable <see cref="ParameterExpression"/> in the compiler
        /// </summary>
        /// <typeparam name="T">The type of the variable</typeparam>
        /// <param name="name">The name of the variable</param>
        /// <returns>The index of the <see cref="ParameterExpression"/> registered in the general container</returns>
        int Variable<T>(string name);
        int Variable(Type type, string name);

        /// <summary>
        /// Register a <see cref="ConstantExpression"/> with the specified value in the compiler
        /// </summary>
        /// <param name="value">The value to create the <see cref="ConstantExpression"/></param>
        /// <returns>The index of the <see cref="ConstantExpression"/> registered in the general container</returns>
        int Constant(object value);

        /// <summary>
        /// Register a property <see cref="MemberExpression"/> of an instance in the compiler
        /// </summary>
        /// <param name="paramIndex">The index of a variable or parameter <see cref="ParameterExpression"/> registered
        /// in the general container to get the property from</param>
        /// <param name="info">The <see cref="PropertyInfo"/> of the property to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container</returns>
        int Property(int paramIndex, PropertyInfo info);

        /// <summary>
        /// Register a property <see cref="MemberExpression"/> of a static class in the compiler
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/> of the static property to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container></returns>
        int StaticProperty(PropertyInfo info);

        /// <summary>
        /// Register a field <see cref="MemberExpression"/> of an instance in the compiler
        /// </summary>
        /// <param name="paramIndex">The index of a variable or parameter <see cref="ParameterExpression"/> registered
        /// in the general container to get the property from</param>
        /// <param name="info">The <see cref="PropertyInfo"/> of the field to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container</returns>
        int Field(int paramIndex, FieldInfo info);

        /// <summary>
        /// Register a field <see cref="MemberExpression"/> of a static class in the compiler
        /// </summary>
        /// <param name="info">The <see cref="PropertyInfo"/> of the static field to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container></returns>
        int StaticField(FieldInfo info);

        /// <summary>
        /// Get the index of a registered parameter or variable <see cref="ParameterExpression"/>
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="ParameterExpression"/> to get</typeparam>
        /// <param name="name">>The name of the <see cref="ParameterExpression"/> to get</param>
        /// <returns>The index of the <see cref="ParameterExpression"/> registered in the general container</returns>
        int GetParamIndex<T>(string name);

        /// <summary>
        /// Try to get the index of a registered parameter or variable <see cref="ParameterExpression"/>
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="ParameterExpression"/> to get</typeparam>
        /// <param name="name">>The name of the <see cref="ParameterExpression"/> to get</param>
        /// <param name="paramIndex">The index of the <see cref="ParameterExpression"/> registered in the general container if it exists
        /// </param>
        /// <returns>True if the parameter exists, else it returns false</returns>
        bool TryGetParamIndex<T>(string name, out int paramIndex);

        /// <summary>
        /// Get the index of a registered property <see cref="MemberExpression"/>
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="MemberExpression"/> to get</typeparam>
        /// <param name="info">>The <see cref="PropertyInfo"/> of the <see cref="MemberExpression"/> to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container</returns>
        int GetPropertyIndex(PropertyInfo info);

        /// <summary>
        /// Get the index of a registered field <see cref="MemberExpression"/>
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="MemberExpression"/> to get</typeparam>
        /// <param name="info">>The <see cref="FieldInfo"/> of the <see cref="MemberExpression"/> to get</param>
        /// <returns>The index of the <see cref="MemberExpression"/> registered in the general container</returns>
        int GetFieldIndex(FieldInfo info);
        #endregion

        #region Operations

        /// <summary>
        /// Register an assign <see cref="BinaryExpression"/> in the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="Expression"/> registered in the general container</param>
        /// <param name="rightIndex">The index of the right <see cref="Expression"/> registered in the general container</param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int Assign(int leftIndex, int rightIndex);

        /// <summary>
        /// Register a post-increment assign <see cref="UnaryExpression"/> in the compiler
        /// </summary>
        /// <param name="paramIndex">The index of the <see cref="ParameterExpression"/> registered in the general container to
        /// post-increment assign</param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int PostIncrementAssign(int paramIndex);

        /// <summary>
        /// Register a post-decrement assign <see cref="UnaryExpression"/> in the compiler
        /// </summary>
        /// <param name="paramIndex">The index of the <see cref="ParameterExpression"/> registered in the general container to
        /// post-decrement assign</param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int PostDecrementAssign(int paramIndex);

        /// <summary>
        /// Register a convert <see cref="UnaryExpression"/> in the compiler if <see cref="T"/> is not a value type, else it register an unbox
        /// <see cref="UnaryExpression"/>
        /// </summary>
        /// <typeparam name="T">The type to convert</typeparam>
        /// <param name="valueIndex">The index of an <see cref="Expression"/> instance registered in the general container that
        /// you want to convert</param>
        /// <returns>The index of the <see cref="UnaryExpression"/> registered in the general container</returns>
        int Convert<T>(int valueIndex);

        int Convert(int valueIndex, Type toConvert);

        /// <summary>
        /// Register a convert <see cref="UnaryExpression"/> if the specified type is not a value type, else it register an unbox
        /// <see cref="UnaryExpression"/>
        /// </summary>
        /// <param name="valueIndex">The index of an <see cref="Expression"/> instance registered in the general container that
        /// you want to convert</param>
        /// <param name="toType">>The type to convert</param>
        /// <returns>The index of the <see cref="UnaryExpression"/> registered in the general container</returns>
        int CastOrUnbox(int valueIndex, Type toType);
        #endregion

        /// <summary>
        /// Register a <see cref="NewExpression"/> of the parameter-less constructor of <see cref="T"/>
        /// </summary>
        /// <returns>The index of the <see cref="NewExpression"/> registered in the general container</returns>
        int New<T>();

        /// <summary>
        /// Register a <see cref="NewExpression"/> of the parameter-less constructor of the specified type
        /// </summary>
        /// <param name="type">The <see cref="Type"/> of the object to create a new expression for</param>
        /// <returns>The index of the <see cref="NewExpression"/> registered in the general container</returns>
        int New(Type type);

        int New(ConstructorInfo info, params int[] argsIndexes);

        /// <summary>
        /// Register a <see cref="MethodCallExpression"/> of a method in the compiler
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo"/> to call</param>
        /// <param name="targetIndex">The index of the <see cref="Expression"/> instance to call the method from</param>
        /// <param name="argsIndexes">The indexes of the <see cref="Expression"/> to use as args for the method</param>
        /// <returns>The index of the <see cref="MethodCallExpression"/> registered in the general container</returns>
        int Call(MethodInfo method, int targetIndex, params int[] argsIndexes);

        /// <summary>
        /// Register a <see cref="MethodCallExpression"/> of a static method in the compiler
        /// </summary>
        /// <param name="method">The static <see cref="MethodInfo"/> to call</param>
        /// <param name="argsIndexes">The indexes of the <see cref="Expression"/> to use as args for the method</param>
        /// <returns>The index of the <see cref="MethodCallExpression"/> registered in the general container</returns>
        int StaticCall(MethodInfo method, params int[] argsIndexes);

        /// <summary>
        /// Register an <see cref="IndexExpression"/> of an array access in the compiler
        /// </summary>
        /// <param name="paramArrayIndex">The index of the <see cref="ParameterExpression"/> array to access</param>
        /// <param name="toAccessIndexes">The indexes of the <see cref="ParameterExpression"/> representing the index to access in the array</param>
        /// <returns>The index of the <see cref="IndexExpression"/> registered in the general container</returns>
        int ArrayAccess(int paramArrayIndex, params int[] toAccessIndexes);

        /// <summary>
        /// Create a for block this way :
        /// <code>
        /// var <paramref name="loopVarIndex"/> = <paramref name="initValueIndex"/>;
        /// while(true)
        /// {
        ///     if(<paramref name="conditionIndex"/>)
        ///     {
        ///         <paramref name="loopContentIndexes"/>
        ///         <paramref name="incrementIndex"/>
        ///     }
        ///     else
        ///     {
        ///         break;
        ///     }
        /// }</code>
        /// </summary>
        /// <param name="loopVarIndex">"i" var on a for loop</param>
        /// <param name="initValueIndex">the value to initialize the <see cref="loopVarIndex"/></param>
        /// <param name="conditionIndex">the condition to check to know if the loop must break or not</param>
        /// <param name="incrementIndex">the incrementation to do after the loop content execution</param>
        /// <param name="loopContentIndexes">the content to execute on the loop</param>
        /// <returns>the index of the for block created with the values specified</returns>
        int For(int loopVarIndex, int initValueIndex, int conditionIndex, int incrementIndex, params int[] loopContentIndexes);

        // TODO: doc
        int For(int loopVarIndex, int maxIterationsIndex, params int[] loopContentIndexes);

        // TODO: doc
        int Foreach(int loopVarIndex, int collectionIndex, params int[] loopContentIndexes);

        /// <summary>
        /// Register a greater-than <see cref="BinaryExpression"/> in the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="ParameterExpression"/> to compare</param>
        /// <param name="rightIndex">The index of the right <see cref="ParameterExpression"/> to compare></param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int GreaterThan(int leftIndex, int rightIndex);

        /// <summary>
        /// Register a less-than <see cref="BinaryExpression"/> in the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="ParameterExpression"/> to compare</param>
        /// <param name="rightIndex">The index of the right <see cref="ParameterExpression"/> to compare></param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int LessThan(int leftIndex, int rightIndex);

        /// <summary>
        /// Register an assign <see cref="BinaryExpression"/> in the content container of the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="Expression"/> registered in the general container</param>
        /// <param name="rightIndex">The index of the right <see cref="Expression"/> registered in the general container</param>
        IExpressionCompiler<TDelegate> EmitAssign(int leftIndex, int rightIndex);

        /// <summary>
        /// Register a <see cref="MethodCallExpression"/> of a method in the content container of the compiler
        /// </summary>
        /// <param name="method">The <see cref="MethodInfo"/> to call</param>
        /// <param name="targetIndex">The index of the <see cref="Expression"/> instance to call the method from</param>
        /// <param name="argsIndexes">The indexes of the <see cref="Expression"/> to use as args for the method</param>
        IExpressionCompiler<TDelegate> EmitCall(MethodInfo method, int targetIndex, params int[] argsIndexes);

        /// <summary>
        /// Register a <see cref="MethodCallExpression"/> of a static method in the content container of the compiler
        /// </summary>
        /// <param name="method">The static <see cref="MethodInfo"/> to call</param>
        /// <param name="argsIndexes">The indexes of the <see cref="Expression"/> to use as args for the method</param>
        IExpressionCompiler<TDelegate> EmitStaticCall(MethodInfo method, params int[] argsIndexes);

        /// <summary>
        /// Register a return <see cref="GotoExpression"/> in the content container of the compiler
        /// </summary>
        /// <typeparam name="T">The type of the returned value</typeparam>
        /// <param name="valueIndex">The index of the <see cref="Expression"/> value to return</param>
        /// <returns></returns>
        IExpressionCompiler<TDelegate> EmitReturn<T>(int valueIndex);

        /// <summary>
        /// Register a <see cref="Expression"/> registered in the general container to the content container of th compiler
        /// </summary>
        /// <param name="expressionIndex">The index of the <see cref="Expression"/> registered in the general container</param>
        /// <returns></returns>
        IExpressionCompiler<TDelegate> Emit(int expressionIndex);

        /// <summary>
        /// Create an <see cref="LambdaExpression"/> and compile it
        /// </summary>
        /// <returns>A compiled lambda of <see cref="TDelegate"/></returns>
        TDelegate Compile();

        /// <summary>
        /// Create an <see cref="LambdaExpression"/> 
        /// </summary>
        /// <returns>A lambda of <see cref="TDelegate"/></returns>
        Expression<TDelegate> ToExpression();
    }
}
