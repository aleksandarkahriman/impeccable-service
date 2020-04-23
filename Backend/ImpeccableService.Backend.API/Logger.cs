using System;
using Microsoft.Extensions.Logging;

namespace ImpeccableService.Backend.API
{
    public class Logger<TSource> : Logger.Abstraction.ILogger<TSource>
    {
        private readonly ILogger<TSource> _logger;

        public Logger(ILogger<TSource> logger)
        {
            _logger = logger;
        }

        public void Info(string message) => _logger.LogInformation(message);

        public void Event(string message) => _logger.LogTrace(message);

        public void Debug(string message) => _logger.LogDebug(message);

        public void Warning(Exception exception, string message) => _logger.LogWarning(exception, message);

        public void Warning(string message) => _logger.LogWarning(new Exception(message), message);

        public void Error(Exception exception, string message) => _logger.LogCritical(exception, message);

        public void Error(string message) => _logger.LogCritical(new Exception(message), message);
    }
}
