namespace Astron.Expressions.Specifications
{
    public interface ISpecification<in TInput>
    {
        bool IsSatisfiedBy(TInput input);
    }
}
