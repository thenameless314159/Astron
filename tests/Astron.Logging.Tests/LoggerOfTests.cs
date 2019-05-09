using System;
using System.IO;
using System.Text;
using Astron.Logging.Tests.Mock;
using Xunit;

namespace Astron.Logging.Tests
{
    public class LoggerOfTests
    {
        private FakeLoggingStrategy _firstStrategy;
        private FakeLoggingStrategy _secondStrategy;
        private readonly ILogger<StringBuilder> _logger;

        public LoggerOfTests()
        {
            var config = new LogConfig(true, LogLevel.Debug, LogLevel.Error);
            _firstStrategy = new FakeLoggingStrategy(config);
            _secondStrategy = new FakeLoggingStrategy(config);
            _logger = Logger.CreateBuilder()
                .Register(_firstStrategy, _secondStrategy)
                .Build()
                .CreateLoggerOf<StringBuilder>();
        }

        [Fact]
        public void Log_ShouldLogWithInstanceName()
        {
            _logger.Log(LogLevel.Info, "hello world !");
            _firstStrategy.VerifyInnerLog();
            _secondStrategy.VerifyInnerLog();
        }

        [Fact]
        public void LogT_ShouldDisplayInstance_OnNotNullInstance()
        {
            _logger.Log(LogLevel.Info, "hello world!", new MemoryStream());
            _firstStrategy.VerifyInnerGenericLog();
            _secondStrategy.VerifyInnerGenericLog();
        }

        [Fact] public void LogT_ShouldThrow_OnNullInstance()
            => Assert.Throws<ArgumentNullException>(() => _logger.Log<MemoryStream>(LogLevel.Info, "hello world !"));
    }
}
