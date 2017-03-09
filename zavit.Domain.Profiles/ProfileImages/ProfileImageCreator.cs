using System.IO;
using System.Threading.Tasks;
using zavit.Domain.Shared;
using zavit.Domain.Shared.Images;

namespace zavit.Domain.Profiles.ProfileImages
{
    public class ProfileImageCreator : IProfileImageCreator
    {
        readonly IImageResizer _imageResizer;
        readonly IGuid _guid;
        readonly IProfileImageStorage _profileImageStorage;
        public const int ProfileImageDimension = 200;

        public ProfileImageCreator(IImageResizer imageResizer, IGuid guid, IProfileImageStorage profileImageStorage)
        {
            _imageResizer = imageResizer;
            _guid = guid;
            _profileImageStorage = profileImageStorage;
        }

        public async Task<string> Create(Stream image)
        {
            if (image == null)
                return null;

            var imageFile = _imageResizer.ResizeImageToMinimum(image, ProfileImageDimension, ProfileImageDimension);

            var imageName = _guid.NewGuidString();

            await _profileImageStorage.SaveImage(imageName, imageFile);

            return imageName;
        }
    }
}