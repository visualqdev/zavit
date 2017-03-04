using System;
using zavit.Infrastructure.Storage.Azure;

namespace zavit.Infrastructure.Logging.Targets
{
    public class LogEntry : TableStorageEntityBase
    {
        public string Message { get; set; }
        public string Level { get; set; }
        public string LoggerName { get; set; }
    }
}