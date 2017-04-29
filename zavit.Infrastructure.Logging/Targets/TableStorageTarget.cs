using System;
using NLog;
using NLog.Common;
using NLog.Targets;
using zavit.Domain.Shared;
using zavit.Infrastructure.Storage;

namespace zavit.Infrastructure.Logging.Targets
{
    public class TableStorageTarget : Target
    {
        readonly ILoggingSettings _loggingSettings;
        readonly ITableStorage _tableStorage;
        readonly IDateTime _dateTime;
        readonly IGuid _guid;

        public TableStorageTarget(ILoggingSettings loggingSettings, ITableStorage tableStorage, IDateTime dateTime, IGuid guid)
        {
            _loggingSettings = loggingSettings;
            _tableStorage = tableStorage;
            _dateTime = dateTime;
            _guid = guid;
        }

        protected override void Write(LogEventInfo loggingEvent)
        {
            try
            {
                var logEntry = new LogEntry()
                {
                    PartitionKey = _dateTime.UtcNow.ToString("yyyy-MM"),
                    RowKey = $"{(DateTime.MaxValue.Ticks - _dateTime.UtcNow.Ticks):D19}-{_guid.NewGuidString()}",
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