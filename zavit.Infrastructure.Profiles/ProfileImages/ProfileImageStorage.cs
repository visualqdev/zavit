using System.IO;
using System.Threading.Tasks;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Infrastructure.Storage;

namespace zavit.Infrastructure.Profiles.ProfileImages
{
    public class ProfileImageStorage : IProfileImageStorage
    {
        readonly IFileStorage _fileStorage;
        readonly IStorageConfig _storageConfig;
        public const string ContainerName = "profileimages";

        public ProfileImageStorage(IFileStorage fileStorage, IStorageConfig storageConfig)
        {
            _fileStorage = fileStorage;
            _storageConfig = storageConfig;
        }

        public async Task SaveImage(string imageName, Stream image)
        {
            await _fileStorage.Upload(ContainerName, $"{imageName}.jpg", image);
        }

        public string ImageUrl(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return null;

            return $"{_storageConfig.StorageUrl}{ContainerName}/{imageName}.jpg";
        }
    }
}