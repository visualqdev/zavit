using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Infrastructure.Storage.Azure;

namespace zavit.Infrastructure.Storage
{
    public interface ITableStorage
    {
        Task<T> GetTableEntity<T>(string tableName, string partition, string rowKey) where T : TableStorageEntityBase, new();

        Task<IEnumerable<T>> GetTableEntities<T>(string tableName, string partition, int take, string startingFromRowKey = null) where T : TableStorageEntityBase, new();

        Task SaveTableEntity<T>(T tableEntity, string tableName) where T : TableStorageEntityBase, new();
    }
}