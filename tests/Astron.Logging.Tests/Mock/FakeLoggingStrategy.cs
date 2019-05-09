using System;
using System.Collections.Generic;
using System.Text;
using Astron.Logging.Strategy;
using Moq;

namespace Astron.Logging.Tests.Mock
{
    public class FakeLoggingStrategy : LoggingStrategy
    {
        private Mock<FakeLoggingStrategy> _mock;

        public FakeLoggingStrategy(LogConfig config) : base(config)
            => _mock = new Mock<FakeLoggingStrategy>(config);

        protected override void Log(LogLevel level, string formattedHeader, string message)
            => _mock.Object.Log(level, formattedHeader, message);

        protected override void Log<T>(LogLevel level, string formattedHeader, string message, T instance)
            => _mock.Object.Log(level, formattedHeader, message, instance);

        public override void Save()
            => _mock.Object.Save();

        protected override void OnMinLevel(LogLevel level, string formattedHeader, string message)
            => _mock.Object.OnMinLevel(level, formattedHeader, message);

        protected override void OnMaxLevel(LogLevel level, string formattedHeader, string message)
        {
            base.OnMaxLevel(level, formattedHeader, message);
            _mock.Object.OnMaxLevel(level, formattedHeader, message);
        }

        public void VerifySave() => _mock.Verify(s => s.Save());

        public void VerifyInnerLog()
            => _mock.Verify(
                s => s.Log(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>()));

        public void VerifyInnerGenericLog()
            => _mock.Verify(
                s => s.Log(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>()));

        public void VerifyOnMinLevel()
            => _mock.Verify(s => s.OnMinLevel(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>()));

        public void VerifyOnMaxLevel()
            => _mock.Verify(s => s.OnMaxLevel(It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<string>()));
    }
}
