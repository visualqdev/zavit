using zavit.Domain.Profiles;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.Profiles
{
    public class ProfileDtoFactory : IProfileDtoFactory
    {
        readonly IProfileImageStorage _profileImageStorage;

        public ProfileDtoFactory(IProfileImageStorage profileImageStorage)
        {
            _profileImageStorage = profileImageStorage;
        }

        public ProfileDto CreateItem(Profile profile, int accountId)
        {
            var profileImageUrl = _profileImageStorage.ImageUrl(profile.ProfileImage);
            
            return new ProfileDto
            {
                DisplayName = profile.DisplayName,
                Email = profile.Email,
                Gender = profile.Gender,
                About = profile.About,
                AccountId = accountId,
                ProfileImageUrl = profileImageUrl
            };
        }
    }
}