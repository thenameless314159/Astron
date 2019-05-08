using System;
using System.Collections.Generic;
using System.Text;
using Astron.Expressions.Specifications;

namespace Astron.Expressions.Helpers
{
    public static class SpecificationExtensions
    {
        public static NotSpecification<T> Not<T>(this BaseSpecification<T> spec)
            => new NotSpecification<T>(spec);
    }
}
