using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.Profiles;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Profiles
{
    public class ProfileDtoService : IProfileDtoService
    {
        readonly IProfileUpdateFactory _profileUpdateFactory;
        readonly IProfileService _profileService;
        readonly IProfileDtoFactory _profileDtoFactory;
        readonly IUserContext _userContext;

        public ProfileDtoService(IProfileUpdateFactory profileUpdateFactory, IProfileService profileService, IProfileDtoFactory profileDtoFactory, IUserContext userContext)
        {
            _profileUpdateFactory = profileUpdateFactory;
            _profileService = profileService;
            _profileDtoFactory = profileDtoFactory;
            _userContext = userContext;
        }

        public ProfileDto Update(ProfileDto profiledDto)
        {
            var account = _userContext.Account;
            var profileUpdate = _profileUpdateFactory.CreateItem(profiledDto);
            var updatedProfile = _profileService.UpdateProfile(profileUpdate, account.Profile);
            return _profileDtoFactory.CreateItem(updatedProfile, account.Id);
        }

        public ProfileDto GetMyProfile()
        {
            var account = _userContext.Account;
            return _profileDtoFactory.CreateItem(account.Profile, account.Id);
        }
    }
}