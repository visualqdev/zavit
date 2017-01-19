using NLog;
using NLog.Config;
using NLog.Targets;

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
                config.AddTarget("debugger", debuggerTarget);
                debuggerTarget.Layout = @"${date:universalTime=true:format=yyyy-MM-dd HH\:mm\:ss.fff} | ${level} | ${logger} | ${message}";
                var debuggerLogRule = new LoggingRule("*", LogLevel.Debug, debuggerTarget);
                config.LoggingRules.Add(debuggerLogRule);
            }

            LogManager.Configuration = config;
        }
    }
}
