namespace zavit.Infrastructure.Storage
{
    public interface IStorageConfig
    {
        string AzureStorageConnectionString { get; }
        int QueueMessageVisibilityTimeoutSeconds { get; }
        string StorageUrl { get; }
    }
}