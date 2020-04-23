using System;
using Microsoft.Extensions.Logging;
using ILogger = Logger.Abstraction.ILogger;

namespace ImpeccableService.Backend.API
{
    public class Logger : ILogger
    {
        private readonly ILogger<Logger> _logger;

        public Logger(ILogger<Logger> logger)
        {
            _logger = logger;
        }

        public void Info<TSource>(string message)
        {
            _logger.LogInformation(message);
        }

        public void Event<TSource>(string message)
        {
            throw new NotImplementedException();
        }

        public void Debug<TSource>(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning<TSource>(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Warning<TSource>(string message)
        {
            throw new NotImplementedException();
        }

        public void Error<TSource>(Exception exception, string message)
        {
            throw new NotImplementedException();
        }

        public void Error<TSource>(string message)
        {
            throw new NotImplementedException();
        }
    }
}
