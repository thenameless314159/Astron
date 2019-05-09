using System;
using System.Collections.Generic;
using System.Text;
using Astron.Logging.Extensions;

namespace Astron.Logging.Strategy
{
    public abstract class LoggingStrategy : ILoggingStrategy
    {
        private readonly LogConfig _config;

        protected LoggingStrategy(LogConfig config) => _config = config;

        protected abstract void Log(LogLevel level, string formattedHeader, string message);
        protected abstract void Log<T>(LogLevel level, string formattedHeader, string message, T instance);
        protected abstract void OnMinLevel(LogLevel level, string formattedHeader, string message);

        protected virtual void OnMaxLevel(LogLevel level, string formattedHeader, string message) => Save();

        protected string FormatHeader(LogLevel level)
        {
            var date = _config.PrintDate
                ? $"[{DateTime.Now:dd-MM-yyyy | HH:mm:ss}]"
                : string.Empty;

            return date + $"[{level.ToString().Capitalize()}]";
        }

        public void Log(LogLevel level, string message)
        {
            var formattedHeader = FormatHeader(level);
            if (level < _config.MinLevel)
            {
                OnMinLevel(level, formattedHeader, message);
                return;
            }
            if (level >= _config.MaxLevel)
            {
                OnMaxLevel(level, formattedHeader, message);
                return;
            }

            Log(level, formattedHeader, message);
        }

        public void Log<T>(LogLevel level, string message, T instance = default)
        {
            if (instance == null)
            {
                Log(level, $"<{typeof(T).Name}> {message}");
                return;
            }

            var formattedHeader = FormatHeader(level);
            if (level < _config.MinLevel)
            {
                OnMinLevel(level, formattedHeader, message);
                return;
            }
            if (level >= _config.MaxLevel)
            {
                OnMaxLevel(level, formattedHeader, message);
                return;
            }

            Log(level, formattedHeader, message, instance);
        }

        public abstract void Save();
    }
}
