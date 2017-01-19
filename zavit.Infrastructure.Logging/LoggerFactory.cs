using System;
using System.Collections.Concurrent;
using zavit.Domain.Shared;

namespace zavit.Infrastructure.Logging
{
    public class LoggerFactory : ILoggerFactory
    {
        static readonly ConcurrentDictionary<string, ILogger> Loggers = new ConcurrentDictionary<string, ILogger>(StringComparer.OrdinalIgnoreCase);

        public ILogger GetLogger(string loggerName)
        {
            var logger = Loggers.GetOrAdd(loggerName, name => new LoggerWrapper(name));
            return logger;
        }
    }
}