using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Astron.Expressions.Specifications;
using Astron.Expressions.Specifications.BuiltIn;

namespace Astron.Expressions.Helpers
{
    /// <summary>
    /// Syntax sugar (with using static) to simplify building algorithm
    /// </summary>
    public static class SpecificationProvider
    {
        public static BaseSpecification<PropertyInfo> IsArray => new IsArray();
        public static BaseSpecification<PropertyInfo> IsString => new IsString();
        public static BaseSpecification<PropertyInfo> IsEnumerable => new IsEnumerable();
        public static BaseSpecification<PropertyInfo> IsCollectionOfCollection => new IsCollectionOfCollection();
        public static BaseSpecification<PropertyInfo> IsGenericEnumerable => new IsGenericEnumerable();
        public static BaseSpecification<PropertyInfo> IsGenericCollection => new IsGenericCollection();
        public static BaseSpecification<PropertyInfo> IsGenericOfGeneric => new IsGenericOfGeneric();
        public static BaseSpecification<PropertyInfo> IsGenericValueType => new IsGenericValueType();
        public static BaseSpecification<PropertyInfo> IsGenericString => new IsGenericString();
        public static BaseSpecification<PropertyInfo> IsGeneric => new IsGeneric();
        public static BaseSpecification<PropertyInfo> IsPrimitive => new IsPrimitive();
        public static BaseSpecification<PropertyInfo> IsValueType => new IsValueType();
        public static BaseSpecification<PropertyInfo> IsStringElement => new IsStringElement();
        public static BaseSpecification<PropertyInfo> IsValueTypeElement => new IsValueTypeElement();
        public static BaseSpecification<PropertyInfo> IsPrimitiveElement => new IsPrimitiveElement();
        public static BaseSpecification<PropertyInfo> IsValueTypeOfValueType => new IsValueTypeOfValueType();
        public static BaseSpecification<PropertyInfo> IsStringOfString => new IsStringOfString();

        public static BaseSpecification<PropertyInfo> IsNotArray => new IsArray().Not();
        public static BaseSpecification<PropertyInfo> IsNotString => new IsString().Not();
        public static BaseSpecification<PropertyInfo> IsNotEnumerable => new IsEnumerable().Not();
        public static BaseSpecification<PropertyInfo> IsNotCollectionOfCollection => new IsCollectionOfCollection().Not();
        public static BaseSpecification<PropertyInfo> IsNotGenericEnumerable => new IsGenericEnumerable().Not();
        public static BaseSpecification<PropertyInfo> IsNotGenericCollection => new IsGenericCollection().Not();
        public static BaseSpecification<PropertyInfo> IsNotGenericOfGeneric => new IsGenericOfGeneric().Not();
        public static BaseSpecification<PropertyInfo> IsNotGenericString => new IsGenericString().Not();
        public static BaseSpecification<PropertyInfo> IsNotGenericValueType => new IsGenericValueType().Not();
        public static BaseSpecification<PropertyInfo> IsNotGeneric => new IsGeneric().Not();
        public static BaseSpecification<PropertyInfo> IsNotPrimitive => new IsPrimitive().Not();
        public static BaseSpecification<PropertyInfo> IsNotValueType => new IsValueType().Not();
        public static BaseSpecification<PropertyInfo> IsNotStringElement => new IsStringElement().Not();
        public static BaseSpecification<PropertyInfo> IsNotValueTypeElement => new IsValueTypeElement().Not();
        public static BaseSpecification<PropertyInfo> IsNotPrimitiveElement => new IsPrimitiveElement().Not();
        public static BaseSpecification<PropertyInfo> IsNotValueTypeOfValueType => new IsValueTypeOfValueType().Not();
        public static BaseSpecification<PropertyInfo> IsNotStringOfString => new IsStringOfString().Not();

        public static BaseSpecification<PropertyInfo> IsStringValue => IsNotArray.And(IsString);
        public static BaseSpecification<PropertyInfo> IsPrimitiveValue => IsNotArray.And(IsPrimitive);
        public static BaseSpecification<PropertyInfo> IsValueTypeValue => IsNotArray.And(IsValueType);
        public static BaseSpecification<PropertyInfo> IsStringArray => IsArray.And(IsStringElement);
        public static BaseSpecification<PropertyInfo> IsValueTypeArray => IsArray.And(IsValueTypeElement);
        public static BaseSpecification<PropertyInfo> IsPrimitiveArray => IsArray.And(IsPrimitiveElement);
        public static BaseSpecification<PropertyInfo> IsValueTypeEnumerable => IsGenericValueType.And(IsEnumerable);
        public static BaseSpecification<PropertyInfo> IsValueTypeCollection => IsGenericValueType.And(IsGenericCollection);

        public static BaseSpecification<PropertyInfo> IsNotStringValue => IsNotArray.And(IsNotString);
        public static BaseSpecification<PropertyInfo> IsNotPrimitiveValue => IsNotArray.And(IsNotPrimitive);
        public static BaseSpecification<PropertyInfo> IsNotValueTypeValue => IsNotArray.And(IsNotValueType);
        public static BaseSpecification<PropertyInfo> IsNotStringArray => IsArray.And(IsNotStringElement);
        public static BaseSpecification<PropertyInfo> IsNotValueTypeArray => IsArray.And(IsNotValueTypeElement);
        public static BaseSpecification<PropertyInfo> IsNotPrimitiveArray => IsArray.And(IsNotPrimitiveElement);
        public static BaseSpecification<PropertyInfo> IsNotValueTypeEnumerable => IsNotGenericValueType.And(IsEnumerable);
        public static BaseSpecification<PropertyInfo> IsNotValueTypeCollection => IsNotGenericValueType.And(IsGenericCollection);
    }
}
