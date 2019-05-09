using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using Astron.Logging.Strategy;

namespace Astron.Logging
{
    public partial class Logger : ILogger
    {
        private ImmutableArray<ILoggingStrategy> _strategies;

        public ILogger<T> CreateLoggerOf<T>() => new LoggerOf<T>(this);

        internal Logger(ImmutableArray<ILoggingStrategy> strategies)
            => _strategies = strategies;

        public void Log(LogLevel level, string message) {
            foreach(var strategy in _strategies) strategy.Log(level, message);
        }

        public void Log<T>(LogLevel level, string message, T instance = default) {
            foreach(var strategy in _strategies) strategy.Log(level, message, instance);
        }

        public void Save() { foreach(var strategy in _strategies) strategy.Save(); }
    }
}
