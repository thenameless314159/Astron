using System.Reflection;
using Astron.Size.Expressions;

namespace Astron.Size.Matching.Handlers
{
    public class OnPrimitiveArray<TClass, TComp> : ISizeMatchingHandler<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        public virtual void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var sizeOfLenConst = exprCompiler.Constant(4);
            var paramValue = exprCompiler.GetParamIndex<TClass>("value");
            var paramSizing = exprCompiler.GetParamIndex<ISizing>("sizing");
            var size = exprCompiler.GetParamIndex<int>("size");

            // first create an expression of the length property of the array
            var prop = exprCompiler.Property(paramValue, pi);
            var lengthProp = exprCompiler.Property(prop, pi.PropertyType.GetProperty("Length"));

            // then create an expression that call the size of method of the element type of the array
            var callSizeOf = exprCompiler.CallSizeOf(pi.PropertyType.GetElementType(), paramSizing);

            // finally create an expression that multiply the size of the element type by the length of the array
            var multiply = exprCompiler.Multiply(callSizeOf, lengthProp);

            var addAssignLen = exprCompiler.AddAssign(size, sizeOfLenConst);
            var addAssignValue = exprCompiler.AddAssign(size, multiply);
            exprCompiler.Emit(addAssignLen);
            exprCompiler.Emit(addAssignValue);
        }
    }
}
