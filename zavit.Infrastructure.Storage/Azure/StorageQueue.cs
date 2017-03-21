using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace zavit.Infrastructure.Storage.Azure
{
    public class StorageQueue : IStorageQueue
    {
        readonly IStorageConfig _storageConfig;

        public StorageQueue(IStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }

        public string GetMessage(string queueName)
        {
            // Retrieve storage account from connection string
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the queue client
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            var queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();

            // Get the next message
            var retrievedMessage = queue.GetMessage();

            if (retrievedMessage == null)
                return null;

            var messageContent = retrievedMessage.AsString;

            //Process the message in less than 30 seconds, and then delete the message
            queue.DeleteMessage(retrievedMessage);

            return messageContent;
        }

        public IEnumerable<T> GetMessages<T>(string queueName, int take, IQueueMessageDeserializer<T> deserializer) where T : class
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the queue client
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            var queue = queueClient.GetQueueReference(queueName);

            queue.CreateIfNotExists();

            // Get the next message
            var retrievedMessages = queue.GetMessages(take, TimeSpan.FromMinutes(_storageConfig.QueueMessageVisibilityTimeoutSeconds));

            if (retrievedMessages == null) yield break;

            var messagesEnumerator = retrievedMessages.GetEnumerator();

            while (messagesEnumerator.MoveNext())
            {
                queue.DeleteMessage(messagesEnumerator.Current);
                var deserialized = deserializer.Deserialize(messagesEnumerator.Current.AsBytes);
                yield return deserialized;
            }
        }

        public void Insert(string queuename, string queueMessage)
        {
            // Retrieve storage account from connection string.
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the queue client.
            var queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            var queue = queueClient.GetQueueReference(queuename);

            // Create the queue if it doesn't already exist.
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            var message = new CloudQueueMessage(queueMessage);
            queue.AddMessage(message);
        }
    }
}