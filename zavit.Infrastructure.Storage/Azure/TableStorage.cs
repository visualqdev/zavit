using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using zavit.Infrastructure.Storage.Extensions;

namespace zavit.Infrastructure.Storage.Azure
{
    public class TableStorage : ITableStorage
    {
        readonly IStorageConfig _storageConfig;

        public TableStorage(IStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }

        public async Task<T> GetTableEntity<T>(string tableName, string partition, string rowKey) where T : TableStorageEntityBase, new()
        {
            var table = GetStorageTable(tableName);

            var retrieveOperation = TableOperation.Retrieve<TableStorageEntityAdapter<T>>(partition, rowKey);
            
            var retrievedResult = await table.ExecuteAsync(retrieveOperation);

            return ((TableStorageEntityAdapter<T>)retrievedResult.Result).InnerObject;
        }

        public async Task<IEnumerable<T>> GetTableEntities<T>(string tableName, string partition, int take, string query = null) where T : TableStorageEntityBase, new()
        {
            var table = GetStorageTable(tableName);

            var partitionQuery = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partition);

            var rangeQuery = new TableQuery<TableStorageEntityAdapter<T>>();

            rangeQuery = rangeQuery.Where(!string.IsNullOrEmpty(query) ? TableQuery.CombineFilters(partitionQuery, TableOperators.And, query) : partitionQuery);
            rangeQuery = rangeQuery.Take(take);

            var results = await table.ExecuteQueryAsync(rangeQuery);

            return results.Select(r => r.InnerObject);
        }

        public async Task SaveTableEntity<T>(T tableEntity, string tableName) where T : TableStorageEntityBase, new()
        {
            var table = GetStorageTable(tableName);

            var tableEntityAdapter = new TableStorageEntityAdapter<T>(tableEntity);
            var insertOperation = TableOperation.Insert(tableEntityAdapter);
            await table.ExecuteAsync(insertOperation);
        }

        CloudTable GetStorageTable(string tableName)
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the table.
            var table = tableClient.GetTableReference(tableName);

            return table;
        }
    }
}