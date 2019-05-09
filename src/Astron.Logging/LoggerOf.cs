using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Logging
{
    public class LoggerOf<TClass> : ILogger<TClass>
    {
        private readonly ILogger _logger;
        private readonly string _className;

        internal LoggerOf(ILogger logger)
        {
            _logger = logger;
            _className = typeof(TClass).Name;
        }

        public void Log(LogLevel level, string message)
            => _logger.Log(level, $"<{_className}> {message}");

        public void Log<T>(LogLevel level, string message, T instance)
        {
            if(instance == null) throw new ArgumentNullException(nameof(instance));
            _logger.Log(level, message, instance);
        }

        public void Save() => _logger.Save();
    }
}
