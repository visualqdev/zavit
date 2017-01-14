using System.IO;
using zavit.Domain.Shared.Images;

namespace zavit.Domain.Profiles.Registration
{
    public class ProfileImageCreator : IProfileImageCreator
    {
        readonly IImageResizer _imageResizer;
        public const int ProfileImageDimension = 200;

        public ProfileImageCreator(IImageResizer imageResizer)
        {
            _imageResizer = imageResizer;
        }

        public ProfileImage Create(Stream image)
        {
            if (image == null)
                return null;

            var imageFile = _imageResizer.ResizeImageToMinimum(image, ProfileImageDimension, ProfileImageDimension);

            return new ProfileImage
            {
                ImageFile = imageFile
            };
        }
    }
}