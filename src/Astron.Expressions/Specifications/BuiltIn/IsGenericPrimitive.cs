﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Helpers;

namespace Astron.Expressions.Specifications.BuiltIn
{
    public class IsGenericPrimitive : Specification<PropertyInfo>
    {
        public IsGenericPrimitive() : base(p => PrimitiveTypes.Primitives.Contains(p.PropertyType.GenericTypeArguments[0]))
        {
        }
    }
}
