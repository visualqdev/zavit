using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace zavit.Infrastructure.Storage.Azure
{
    public class FileStorage : IFileStorage
    {
        readonly IStorageConfig _storageConfig;

        public FileStorage(IStorageConfig storageConfig)
        {
            _storageConfig = storageConfig;
        }

        public MemoryStream Download(string containerName, string path)
        {
            var blockBlob = GetCloudBlockBlob(containerName, path);

            var memoryStream = new MemoryStream();
            try
            {
                blockBlob.DownloadToStream(memoryStream);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting blob '{path}'", ex);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public void Upload(string containerName, string path, byte[] file)
        {
            var blockBlob = GetCloudBlockBlob(containerName, path);

            try
            {
                blockBlob.UploadFromByteArray(file, 0, file.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading blob '{path}'", ex);
            }
        }

        public void Upload(string containerName, string path, Stream file)
        {
            var blockBlob = GetCloudBlockBlob(containerName, path);

            try
            {
                blockBlob.UploadFromStream(file);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading blob '{path}'", ex);
            }
        }

        public async Task UploadAsync(string containerName, string path, Stream file)
        {
            var blockBlob = GetCloudBlockBlob(containerName, path);

            try
            {
                await blockBlob.UploadFromStreamAsync(file);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading blob '{path}'", ex);
            }
        }

        public void Delete(string containerName, string path)
        {
            var blockBlob = GetCloudBlockBlob(containerName, path);
            try
            {
                blockBlob.Delete();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting blob '{path}'", ex);
            }
        }

        CloudBlockBlob GetCloudBlockBlob(string containerName, string path)
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            var container = blobClient.GetContainerReference(containerName);

            //create the container if it doesn't exist
            container.CreateIfNotExists();

            // Retrieve reference to a blob named "myblob.txt"
            return container.GetBlockBlobReference(path);
        }
    }
}