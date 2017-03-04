using System.Configuration;
using zavit.Infrastructure.Logging;

namespace zavit.Web.Mvc.Settings
{
    public class LoggingSettings : ILoggingSettings
    {
        bool? _debuggerLogEnabled;
        public bool DebuggerLogEnabled
        {
            get
            {
                if (_debuggerLogEnabled.HasValue) return _debuggerLogEnabled.Value;

                bool isDebuggerEnabled;
                _debuggerLogEnabled = bool.TryParse(ConfigurationManager.AppSettings["Logging.Debugger.IsEnabled"], out isDebuggerEnabled) && isDebuggerEnabled;
                return _debuggerLogEnabled.Value;
            }
        }

        string _logStorageTableName;
        public string LogStorageTableName => _logStorageTableName ?? (_logStorageTableName = ConfigurationManager.AppSettings["Logging.TableStorage.TableName"]);

        bool? _tableStorageLogEnabled;
        public bool TableStorageLogEnabled
        {
            get
            {
                if (_tableStorageLogEnabled.HasValue) return _tableStorageLogEnabled.Value;

                bool isTableStorageEnabled;
                _tableStorageLogEnabled = bool.TryParse(ConfigurationManager.AppSettings["Logging.TableStorage.IsEnabled"], out isTableStorageEnabled) && isTableStorageEnabled;
                return _tableStorageLogEnabled.Value;
            }
        }
    }
}