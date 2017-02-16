using zavit.Infrastructure.Logging;

namespace zavit.Web.Mvc.Settings
{
    public class LoggingSettings : ILoggingSettings
    {
        public bool DebuggerLogEnabled => true;
    }
}