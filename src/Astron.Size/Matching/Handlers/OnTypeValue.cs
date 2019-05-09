using System.Reflection;
using Astron.Size.Expressions;

namespace Astron.Size.Matching.Handlers
{
    public class OnTypeValue<TClass, TComp> : ISizeMatchingHandler<TClass, TComp>
        where TComp : ICalculateFuncCompilerOf<TClass>
    {
        public virtual void OnMatch(PropertyInfo pi, TComp exprCompiler)
        {
            var paramValue = exprCompiler.GetParamIndex<TClass>("value");
            var paramSizing = exprCompiler.GetParamIndex<ISizing>("sizing");
            var size = exprCompiler.GetParamIndex<int>("size");

            var prop = exprCompiler.Property(paramValue, pi);
            var callSizeOf = exprCompiler.CallSizeOf(pi.PropertyType, paramSizing, prop);

            var addAssignValue = exprCompiler.AddAssign(size, callSizeOf);
            exprCompiler.Emit(addAssignValue);
        }
    }
}
