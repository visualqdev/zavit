using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.Profiles;

namespace zavit.Web.Api.DtoFactories.Profiles
{
    public class ProfileDtoFactory : IProfileDtoFactory
    {
        public ProfileDto CreateItem(Profile profile)
        {
            return new ProfileDto
            {
                DisplayName = profile.Account.DisplayName,
                Email = profile.Account.Email,
                Gender = profile.Gender,
                About = profile.About
            };
        }
    }
}