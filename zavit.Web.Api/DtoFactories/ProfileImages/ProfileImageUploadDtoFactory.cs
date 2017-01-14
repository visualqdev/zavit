using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.ProfileImages
{
    public class ProfileImageUploadDtoFactory : IProfileImageUploadDtoFactory
    {
        readonly IProfileImageUrlBuilder _profileImageUrlBuilder;

        public ProfileImageUploadDtoFactory(IProfileImageUrlBuilder profileImageUrlBuilder)
        {
            _profileImageUrlBuilder = profileImageUrlBuilder;
        }

        public ProfileImageUploadDto CreateItem(Profile profile)
        {
            var profileImageUrl = _profileImageUrlBuilder.Build(profile);

            return new ProfileImageUploadDto
            {
                ProfileImageUrl = profileImageUrl
            };
        }
    }
}