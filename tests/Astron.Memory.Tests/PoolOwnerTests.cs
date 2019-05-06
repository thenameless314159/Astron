using System;
using Moq;
using Moq.Protected;
using Xunit;

namespace Astron.Memory.Tests
{
    public class FakePoolOwner<T> : PoolOwner<T>
    {
        private readonly Mock<FakePoolOwner<T>> _mock;

        public FakePoolOwner(T[] oversized, int length) : base(oversized, length)
        {
            _mock = new Mock<FakePoolOwner<T>>(oversized, length);
        }

        protected override void ReturnToPool(T[] array)
        {
            _mock.Object.ReturnToPool(array);
        }

        public void VerifyReturnToPool() => _mock.Verify(o => o.ReturnToPool(It.IsAny<T[]>()));
    }

    public class PoolOwnerTests
    {
        [Fact]
        public void Ctor_ShouldRun_WhenLengthIsLesserOrEqualThanBufferLen()
        {
            var buffer = new byte[1];
            var owner = new FakePoolOwner<byte>(buffer, 1);
        }

        [Fact]
        public void Ctor_ShouldThrow_WhenLengthIsGreaterThanBufferLen()
        {
            var buffer = new byte[1];
            Assert.Throws<ArgumentOutOfRangeException>(() => new FakePoolOwner<byte>(buffer, 2));
        }

        [Fact]
        public void GetMemory_ShouldThrow_WhenDisposed()
        {
            var buffer = new byte[1];
            var simpleOwner = new FakePoolOwner<byte>(buffer, 1);
            simpleOwner.Dispose();
            Assert.Throws<ObjectDisposedException>(() => simpleOwner.Memory);
        }

        [Fact]
        public void Dispose_ShouldCallReturnToPool()
        {
            var buffer = new byte[1];
            var simpleOwner = new FakePoolOwner<byte>(buffer, 1);

            simpleOwner.Dispose();
            simpleOwner.VerifyReturnToPool();
        }
    }
}
