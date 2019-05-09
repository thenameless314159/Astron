using System;
using System.Collections.Generic;
using System.Text;
using Astron.Size.Cache;
using Astron.Size.Storage;
using Astron.Size.Tests.Models;
using Xunit;

namespace Astron.Size.Tests
{
    public class StorageProviderTests
    {
        public const int PrimitiveArrayLength = 16;
        public const int SizeOfPrimitive = 43;
        public const int SizeOfPrimitiveArray = 68;

        private static readonly ISizing Sizing = new SizingBuilder()
            .Register(StorageProvider.CreateCompiled<Primitive>())
            .Build();

        [Fact] public void CreateCompiled_ShouldHandlePrimitiveValues()
            => Assert.Equal(SizeOfPrimitive, Sizing.SizeOf(new Primitive()));

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValue()
        {
            var storage = StorageProvider.CreateCompiled<TypeValue>();
            Assert.Equal(SizeOfPrimitive, storage.Calculate(Sizing, new TypeValue()));
        }

        [Fact]
        public void CreateCompiled_ShouldHandlePrimitiveArray()
        {
            var storage = StorageProvider.CreateCompiled<PrimitiveArray>();
            Assert.Equal(SizeOfPrimitiveArray, 
                storage.Calculate(Sizing, new PrimitiveArray { Array = new int[PrimitiveArrayLength]}));
        }

        [Fact]
        public void CreateCompiled_ShouldHandleTypeValueArray()
        {
            var storage = StorageProvider.CreateCompiled<TypeValueArray>();
            Assert.Equal(SizeOfPrimitive * 2 + 4, // size of array + length
                storage.Calculate(Sizing, new TypeValueArray
                {
                    Primitives = new [] { new Primitive(), new Primitive()}
                }));
        }

        [Fact]
        public void CreateLazy_ShouldBuildOnFirstCall()
        {
            var builder = new SizingBuilder();
            Assert.Null(LazySizeOfCache<LazyPrimitive>.Builder); // ensure nothing is set yet

            var storage = StorageProvider.CreateLazy<LazyPrimitive>();
            Assert.NotNull(LazySizeOfCache<LazyPrimitive>.Builder); // should be set now
            Assert.Throws<KeyNotFoundException>(() => SizeOfCache<LazyPrimitive>.Calculate); // still not registered, should be null

            builder.Register(storage);
            Assert.NotNull(SizeOfCache<LazyPrimitive>.Calculate); // should be set now

            Assert.Equal(4, builder.Build().SizeOf(new LazyPrimitive()));
        }
    }
}
