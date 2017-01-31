using System;
using NLog;

namespace zavit.Infrastructure.Logging
{
    public class LoggerWrapper : Domain.Shared.ILogger
    {
        readonly Logger _logger;

        public LoggerWrapper(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Warn(Exception exception)
        {
            _logger.Warn(exception);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            _logger.Fatal(exception);
        }
    }
}