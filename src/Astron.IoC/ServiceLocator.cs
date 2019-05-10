using System;
using Astron.Logging;

namespace Astron.IoC
{
    public static class ServiceLocator
    {
        public static IContainer Container
        {
            get =>
                _container ??
                throw new NullReferenceException($"You must set {nameof(Container)} before using {nameof(ServiceLocator)}");
            set
            {
                if (_isContainerInitialized)
                    return;
                _container = value;
                _isContainerInitialized = true;
            }
        }

        public static ILogger Logger
        {
            get =>
                _logger ??
                throw new NullReferenceException($"You must set {nameof(Logger)} before using {nameof(ServiceLocator)}");
            set
            {
                if (_isLoggerInitialized)
                    return;
                _logger = value;
                _isLoggerInitialized = true;
            }
        }

        public static ILogger<T> GetLoggerOf<T>() => ((Logger)_logger)?.CreateLoggerOf<T>();

        private static IContainer _container;
        private static bool _isContainerInitialized;

        private static ILogger _logger;
        private static bool _isLoggerInitialized;
    }
}
