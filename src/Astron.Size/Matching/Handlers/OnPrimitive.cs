using System.Reflection;
using Astron.Size.Expressions;

namespace Astron.Size.Matching.Handlers
{
    public class OnPrimitive<TClass, TComp> : ISizeMatchingHandler<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        public virtual void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var paramSizing = exprCompiler.GetParamIndex<ISizing>("sizing");
            var size = exprCompiler.GetParamIndex<int>("size");

            var callSizeOf = exprCompiler.CallSizeOf(pi.PropertyType, paramSizing);

            var addAssignValue = exprCompiler.AddAssign(size, callSizeOf);
            exprCompiler.Emit(addAssignValue);
        }
    }
}
