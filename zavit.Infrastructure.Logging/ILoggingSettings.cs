namespace zavit.Infrastructure.Logging
{
    public interface ILoggingSettings
    {
        bool DebuggerLogEnabled { get; }
        string LogStorageTableName { get; }
        bool TableStorageLogEnabled { get; }
    }
}