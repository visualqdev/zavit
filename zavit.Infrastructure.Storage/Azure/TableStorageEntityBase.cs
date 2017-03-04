using System;

namespace zavit.Infrastructure.Storage.Azure
{
    public abstract class TableStorageEntityBase
    {
        protected TableStorageEntityBase() { }

        protected TableStorageEntityBase(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string ETag { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}