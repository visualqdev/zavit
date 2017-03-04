using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace zavit.Infrastructure.Storage.Azure
{
    internal class TableStorageEntityAdapter<T> : ITableEntity where T : TableStorageEntityBase, new()
    {
        public TableStorageEntityAdapter()
        {
            InnerObject = new T();
        }

        public T InnerObject { get; set; }

        public TableStorageEntityAdapter(T innerObject)
        {
            InnerObject = innerObject;
        }

        public string PartitionKey
        {
            get { return InnerObject.PartitionKey; }
            set { InnerObject.PartitionKey = value; }
        }

        public string RowKey
        {
            get { return InnerObject.RowKey; }
            set { InnerObject.RowKey = value; }
        }

        public DateTimeOffset Timestamp
        {
            get { return InnerObject.Timestamp; }
            set { InnerObject.Timestamp = value; }
        }

        public string ETag
        {
            get { return InnerObject.ETag; }
            set { InnerObject.ETag = value; }
        }

        public void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            TableEntity.ReadUserObject(InnerObject, properties, operationContext);
        }

        public IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
        {
            return TableEntity.WriteUserObject(InnerObject, operationContext);
        }
    }
}