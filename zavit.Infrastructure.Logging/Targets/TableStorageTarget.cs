using System;
using NLog;
using NLog.Common;
using NLog.Targets;
using zavit.Infrastructure.Storage;

namespace zavit.Infrastructure.Logging.Targets
{
    public class TableStorageTarget : Target
    {
        readonly ILoggingSettings _loggingSettings;
        readonly ITableStorage _tableStorage;

        public TableStorageTarget(ILoggingSettings loggingSettings, ITableStorage tableStorage)
        {
            _loggingSettings = loggingSettings;
            _tableStorage = tableStorage;
        }

        protected override void Write(LogEventInfo loggingEvent)
        {
            try
            {
                var logEntry = new LogEntry
                {
                    Timestamp = loggingEvent.TimeStamp,
                    Message = $"{loggingEvent.FormattedMessage}",
                    Level = loggingEvent.Level.Name,
                    LoggerName = loggingEvent.LoggerName
                };

                _tableStorage.SaveTableEntity(logEntry, _loggingSettings.LogStorageTableName);
            }
            catch (Exception e)
            {
                InternalLogger.Error(
                    $"{GetType().AssemblyQualifiedName}: Could not write log entry to table {_loggingSettings.LogStorageTableName}: {e.Message}", e);
            }
        }
    }
}