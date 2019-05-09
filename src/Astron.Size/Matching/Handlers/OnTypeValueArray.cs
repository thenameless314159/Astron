using System.Reflection;
using Astron.Size.Expressions;

namespace Astron.Size.Matching.Handlers
{
    public class OnTypeValueArray<TClass, TComp> : ISizeMatchingHandler<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        public virtual void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var elementType = pi.PropertyType.GetElementType();

            var sizeOfLenConst = exprCompiler.Constant(4);
            var paramValue = exprCompiler.GetParamIndex<TClass>("value");
            var paramSizing = exprCompiler.GetParamIndex<ISizing>("sizing");
            var size = exprCompiler.GetParamIndex<int>("size");

            // first get the property and create the loop var
            var collection = exprCompiler.Property(paramValue, pi);
            var loopVar = exprCompiler.Variable(elementType, "loopVar");

            // then create the content of the loop
            var callSizeOf = exprCompiler.CallSizeOf(elementType, paramSizing, loopVar);
            var loopContent = exprCompiler.AddAssign(size, callSizeOf); // loop content

            var addAssignLen = exprCompiler.AddAssign(size, sizeOfLenConst);
            var foreachLoop = exprCompiler.Foreach(loopVar, collection, loopContent);
            exprCompiler.Emit(addAssignLen);
            exprCompiler.Emit(foreachLoop);
        }
    }
}
