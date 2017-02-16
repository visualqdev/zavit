using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace zavit.Infrastructure.Logging
{
    public class LoggingConfig
    {
        public static void Configure(ILoggingSettings loggingSettings)
        {
            var config = new LoggingConfiguration();

            var debuggerTarget = new DebuggerTarget();
            

            if (loggingSettings.DebuggerLogEnabled)
            {
                debuggerTarget.Layout = @"${date:universalTime=true:format=yyyy-MM-dd HH\:mm\:ss.fff} | ${level} | ${logger} | ${message}";
                var asyncDebuggerWrapper = new AsyncTargetWrapper(debuggerTarget);
                config.AddTarget("debugger", asyncDebuggerWrapper);
                var debuggerLogRule = new LoggingRule("*", LogLevel.Debug, asyncDebuggerWrapper);
                config.LoggingRules.Add(debuggerLogRule);
            }

            LogManager.Configuration = config;
        }
    }
}
