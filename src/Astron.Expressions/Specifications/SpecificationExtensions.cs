namespace Astron.Expressions.Specifications
{
    public static class SpecificationExtensions
    {
        public static NotSpecification<T> Not<T>(this BaseSpecification<T> spec)
            => new NotSpecification<T>(spec);
    }
}
