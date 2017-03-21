using System.IO;
using System.Threading.Tasks;

namespace zavit.Infrastructure.Storage
{
    public interface IFileStorage
    {
        Task<MemoryStream> Download(string containerName, string path);
        Task Upload(string containerName, string path, byte[] file);
        Task Upload(string containerName, string path, Stream file);
        Task Delete(string containerName, string path);
    }
}