using System;
using System.Linq.Expressions;
using Astron.Expressions;

namespace Astron.Size.Expressions
{
    public interface ICalculateFuncCompilerOf<TClass> : IExpressionCompiler<Func<ISizing, TClass, int>>
    {
        /// <summary>
        /// Register an add assign <see cref="BinaryExpression"/> in the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="Expression"/> registered in the general container</param>
        /// <param name="rightIndex">The index of the right <see cref="Expression"/> registered in the general container</param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int AddAssign(int leftIndex, int rightIndex);
        
        /// <summary>
        /// Register an add assign <see cref="BinaryExpression"/> in the compiler
        /// </summary>
        /// <param name="leftIndex">The index of the left <see cref="Expression"/> registered in the general container</param>
        /// <param name="rightIndex">The index of the right <see cref="Expression"/> registered in the general container</param>
        /// <returns>The index of the <see cref="BinaryExpression"/> registered in the general container</returns>
        int Multiply(int leftIndex, int rightIndex);

        int CallSizeOf(Type type, int sizingIndex);
        int CallSizeOf(Type type, int sizingIndex, int sizedIndex);
    }
}
