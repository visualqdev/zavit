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

        public async Task<MemoryStream> Download(string containerName, string path)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, path);

            var memoryStream = new MemoryStream();
            try
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting blob '{path}'", ex);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task Upload(string containerName, string path, byte[] file)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, path);

            try
            {
                await blockBlob.UploadFromByteArrayAsync(file, 0, file.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading blob '{path}'", ex);
            }
        }

        public async Task Upload(string containerName, string path, Stream file)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, path);

            try
            {
                await blockBlob.UploadFromStreamAsync(file);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading blob '{path}'", ex);
            }
        }

        public async Task Delete(string containerName, string path)
        {
            var blockBlob = await GetCloudBlockBlob(containerName, path);
            try
            {
                await blockBlob.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting blob '{path}'", ex);
            }
        }

        async Task<CloudBlockBlob> GetCloudBlockBlob(string containerName, string path)
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConfig.AzureStorageConnectionString);

            // Create the blob client.
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            var container = blobClient.GetContainerReference(containerName);

            //create the container if it doesn't exist
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob.txt"
            return container.GetBlockBlobReference(path);
        }
    }
}