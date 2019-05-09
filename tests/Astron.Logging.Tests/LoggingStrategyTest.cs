using System.Text;
using Astron.Logging.Tests.Mock;
using Xunit;

namespace Astron.Logging.Tests
{
    public class LoggingStrategyTest
    {
        private FakeLoggingStrategy _strategy;

        public LoggingStrategyTest()
        {
            var config = new LogConfig(true, LogLevel.Debug, LogLevel.Error);
            _strategy = new FakeLoggingStrategy(config);
        }

        [Fact]
        public void Log_ShouldInvokeInnerLogMethod()
        {
            _strategy.Log(LogLevel.Info, "hello world !");
            _strategy.VerifyInnerLog();
        }

        [Fact]
        public void Log_ShouldHandleMinLevel()
        {
            _strategy.Log(LogLevel.Trace, "hello world !");
            _strategy.VerifyOnMinLevel();

            _strategy.Log(LogLevel.Debug, "hello world !");
            _strategy.VerifyInnerLog(); // should not call OnMinLevel
        }

        [Fact]
        public void Log_ShouldHandleMaxLevel()
        {
            _strategy.Log(LogLevel.Fatal, "hello world !");
            _strategy.VerifyOnMaxLevel();
        }

        [Fact]
        public void LogT_ShouldInvokeInnerGenericLog_OnNotNullInstance()
        {
            _strategy.Log(LogLevel.Info, "hello world !", new StringBuilder());
            _strategy.VerifyInnerGenericLog();
        }

        [Fact]
        public void LogT_ShouldInvokeInnerLog_OnNullInstance()
        {
            _strategy.Log<StringBuilder>(LogLevel.Info, "hello world !");
            _strategy.VerifyInnerLog();
        }
    }
}
