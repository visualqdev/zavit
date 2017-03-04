using System.Collections.Generic;

namespace zavit.Infrastructure.Storage
{
    public interface IStorageQueue
    {
        string GetMessage(string queueName);
        IEnumerable<T> GetMessages<T>(string queueName, int take, IQueueMessageDeserializer<T> deserializer) where T : class;
        void Insert(string queuename, string queueMessage);
    }
}