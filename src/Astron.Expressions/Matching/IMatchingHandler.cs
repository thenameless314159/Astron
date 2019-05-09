namespace Astron.Expressions.Matching
{
    public interface IMatchingHandler<in TInput, in TDep>
    {
        void OnMatch(TInput input, TDep dependency);
    }
}
