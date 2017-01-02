using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.DtoFactories.Profiles
{
    public class ProfileDtoFactory : IProfileDtoFactory
    {
        readonly IProfileImageUrlBuilder _profileImageUrlBuilder;

        public ProfileDtoFactory(IProfileImageUrlBuilder profileImageUrlBuilder)
        {
            _profileImageUrlBuilder = profileImageUrlBuilder;
        }

        public ProfileDto CreateItem(Profile profile)
        {
            var profileImageUrl = _profileImageUrlBuilder.Build(profile);
            
            return new ProfileDto
            {
                DisplayName = profile.Account.DisplayName,
                Email = profile.Account.Email,
                Gender = profile.Gender,
                About = profile.About,
                AccountId = profile.Account.Id,
                ProfileImageUrl = profileImageUrl
            };
        }
    }
}