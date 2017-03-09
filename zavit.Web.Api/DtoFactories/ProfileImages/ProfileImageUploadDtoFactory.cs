using zavit.Domain.Profiles;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Web.Api.Dtos.ProfileImages;

namespace zavit.Web.Api.DtoFactories.ProfileImages
{
    public class ProfileImageUploadDtoFactory : IProfileImageUploadDtoFactory
    {
        readonly IProfileImageStorage _profileImageStorage;

        public ProfileImageUploadDtoFactory(IProfileImageStorage profileImageStorage)
        {
            _profileImageStorage = profileImageStorage;
        }

        public ProfileImageUploadDto CreateItem(Profile profile)
        {
            var profileImageUrl = _profileImageStorage.ImageUrl(profile.ProfileImage);

            return new ProfileImageUploadDto
            {
                ProfileImageUrl = profileImageUrl
            };
        }
    }
}