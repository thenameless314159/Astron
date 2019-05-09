using System;
using System.Collections.Generic;
using System.Text;
using Astron.Logging.Tests.Mock;
using Xunit;

namespace Astron.Logging.Tests
{
    public class LoggerTests
    {
        private FakeLoggingStrategy _firstStrategy;
        private FakeLoggingStrategy _secondStrategy;
        private readonly ILogger _logger;

        public LoggerTests()
        {
            var config = new LogConfig(true, LogLevel.Debug, LogLevel.Error);
            _firstStrategy = new FakeLoggingStrategy(config);
            _secondStrategy = new FakeLoggingStrategy(config);
            _logger = Logger.CreateBuilder().Register(_firstStrategy, _secondStrategy).Build();
        }

        [Fact]
        public void Log_ShouldUseAllStrategies()
        {
            _logger.Log(LogLevel.Info, "hello world !");
            _firstStrategy.VerifyInnerLog();
            _secondStrategy.VerifyInnerLog();
        }

        [Fact]
        public void LogT_ShouldUseAllStrategies_OnNullInstance()
        {
            _logger.Log<StringBuilder>(LogLevel.Info, "hello world !");
            _firstStrategy.VerifyInnerLog();
            _secondStrategy.VerifyInnerLog();
        }

        [Fact]
        public void LogT_ShouldUseAllStrategies_OnNotNullInstance()
        {
            _logger.Log(LogLevel.Info, "hello world !", new StringBuilder());
            _firstStrategy.VerifyInnerGenericLog();
            _secondStrategy.VerifyInnerGenericLog();
        }

        [Fact]
        public void Save_ShouldUseAllStrategies()
        {
            _logger.Save();
            _firstStrategy.VerifySave();
            _secondStrategy.VerifySave();
        }

        [Fact]
        public void CreateLoggerOfT_ShouldReturnNewInstance()
        {
            var logger = (Logger) _logger;
            var loggerOfSb = logger.CreateLoggerOf<StringBuilder>();
            Assert.NotNull(loggerOfSb);
        }
    }
}
