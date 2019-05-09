using System;
using Xunit;

namespace Astron.Memory.Tests
{
    public class HeapAllocWithSharedPoolPolicyTests
    {
        private readonly IMemoryPolicy _policy = new HeapAllocWithSharedPoolPolicy();

        [Theory, InlineData(1), InlineData(128), InlineData(2048)]
        public void GetArray_ShouldAllocateNew_WhenSizeIs(int equalTo)
        {
            var array = _policy.GetArray<byte>(equalTo);
            Assert.False(array.IsEmpty);
            Assert.Equal(equalTo, array.Length);
        }

        [Theory, InlineData(-1), InlineData(-128), InlineData(-2048)]
        public void GetArray_ShouldThrow_WhenSizeIs(int equalTo)
            => Assert.Throws<ArgumentOutOfRangeException>(() => _policy.GetArray<byte>(equalTo));

        [Theory, InlineData(1), InlineData(128), InlineData(2048)]
        public void GetOwnedArray_ShouldAllocateArrayPoolOwner_WhenSizeIs(int equalTo)
        {
            using (var array = _policy.GetOwnedArray<byte>(equalTo))
            {
                Assert.False(array.Memory.IsEmpty);
                Assert.Equal(equalTo, array.Memory.Length);
            }
        }

        [Theory, InlineData(-1), InlineData(-128), InlineData(-2048)]
        public void GetOwnedArray_ShouldThrow_WhenSizeIs(int equalTo)
            => Assert.Throws<ArgumentOutOfRangeException>(() => _policy.GetOwnedArray<byte>(equalTo));

        [Fact]
        public void GetOwnedArray_ShouldThrow_WhenArrayHasBeenConsumed()
        {
            var array = _policy.GetOwnedArray<byte>(128);

            // do stuff, better use a using statement

            array.Dispose();
            Assert.Throws<ObjectDisposedException>(() => array.Memory);
        }
    }
}
