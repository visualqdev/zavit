using System.IO;
using System.Threading.Tasks;

namespace zavit.Infrastructure.Storage
{
    public interface IFileStorage
    {
        MemoryStream Download(string containerName, string path);
        void Upload(string containerName, string path, byte[] file);
        void Upload(string containerName, string path, Stream file);
        Task UploadAsync(string containerName, string path, Stream file);
        void Delete(string containerName, string path);
    }
}