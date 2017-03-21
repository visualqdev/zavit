using System.Configuration;
using zavit.Infrastructure.Storage;

namespace zavit.Web.Mvc.Settings
{
    public class StorageConfig : IStorageConfig
    {
        string _azureStorageConnectionString;
        public string AzureStorageConnectionString => _azureStorageConnectionString ?? (_azureStorageConnectionString = ConfigurationManager.AppSettings["Azure.Storage.ConnectionString"]);

        int? _queueMessageVisibilityTimeoutSeconds;
        public int QueueMessageVisibilityTimeoutSeconds 
        {
            get
            {
                if (!_queueMessageVisibilityTimeoutSeconds.HasValue)
                    _queueMessageVisibilityTimeoutSeconds =int.Parse(ConfigurationManager.AppSettings["Azure.Storage.QueueMessageVisibilityTimeoutSeconds"]);

                return _queueMessageVisibilityTimeoutSeconds.Value;
            }
        }

        string _storageUrl;
        public string StorageUrl => _storageUrl ?? (_storageUrl = ConfigurationManager.AppSettings["Azure.Storage.Url"]);
    }
}