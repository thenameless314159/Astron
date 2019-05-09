using System;

namespace Astron.Logging
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
        void Log<T>(LogLevel level, string message, T instance = default);
        void Save();
    }

    public interface ILogger<TClass> : ILogger
    {
    }
}
