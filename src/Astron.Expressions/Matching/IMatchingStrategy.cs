namespace Astron.Expressions.Matching
{
    public interface IMatchingStrategy<in TInput, in TDependency>
    {
        void Process(TInput input, TDependency dependency);
    }
}
