using zavit.Domain.Profiles.Updating;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public class ProfileUpdateFactory : IProfileUpdateFactory
    {
        readonly IUserContext _userContext;

        public ProfileUpdateFactory(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public ProfileUpdate CreateItem(ProfileDto profileDto)
        {
            return new ProfileUpdate
            {
                Account = _userContext.Account,
                DisplayName = profileDto.DisplayName,
                Gender = profileDto.Gender,
                About = profileDto.About,
                Email = profileDto.Email
            };
        }
    }
}