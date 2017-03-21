using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using zavit.Infrastructure.Core;
using zavit.Infrastructure.Logging.Targets;
using zavit.Infrastructure.Storage;

namespace zavit.Infrastructure.Logging
{
    public class LoggingConfig
    {
        public static void Configure(IContainer container)
        {
            var loggingSettings = container.Resolve<ILoggingSettings>();

            var config = new LoggingConfiguration();

            if (loggingSettings.DebuggerLogEnabled)
            {
                var debuggerTarget = new DebuggerTarget
                {
                    Layout = @"${date:universalTime=true:format=yyyy-MM-dd HH\:mm\:ss.fff} | ${level} | ${logger} | ${message}"
                };

                var asyncDebuggerWrapper = new AsyncTargetWrapper(debuggerTarget);
                config.AddTarget("debuggerTarget", asyncDebuggerWrapper);
                var debuggerLogRule = new LoggingRule("*", LogLevel.Debug, asyncDebuggerWrapper);
                config.LoggingRules.Add(debuggerLogRule);
            }

            if (loggingSettings.TableStorageLogEnabled)
            {
                var tableStorageTarget = new TableStorageTarget(loggingSettings, container.Resolve<ITableStorage>());

                var asyncDebuggerWrapper = new AsyncTargetWrapper(tableStorageTarget);
                config.AddTarget("tableStorageTarget", asyncDebuggerWrapper);
                var debuggerLogRule = new LoggingRule("*", LogLevel.Info, asyncDebuggerWrapper);
                config.LoggingRules.Add(debuggerLogRule);
            }

            LogManager.Configuration = config;
        }
    }
}
