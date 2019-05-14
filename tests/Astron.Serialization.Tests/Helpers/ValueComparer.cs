using System;
using System.Collections.Generic;
using System.Text;
using Astron.Serialization.Tests.Models;

namespace Astron.Serialization.Tests.Helpers
{
    public class ValueComparer : EqualityComparerOf<Value>
    {
        public ValueComparer() : base(
            (v1, v2) => 
                v1.B == v2.B 
                && v1.S == v2.S 
                && v1.I == v2.I
                && v1.L == v2.L)
        {
        }
    }
}
