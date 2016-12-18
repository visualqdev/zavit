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
        readonly IProfileRepository _profileRepository;
        readonly IUserContext _userContext;

        public ProfileDtoService(IProfileUpdateFactory profileUpdateFactory, IProfileService profileService, IProfileDtoFactory profileDtoFactory, IProfileRepository profileRepository, IUserContext userContext)
        {
            _profileUpdateFactory = profileUpdateFactory;
            _profileService = profileService;
            _profileDtoFactory = profileDtoFactory;
            _profileRepository = profileRepository;
            _userContext = userContext;
        }

        public ProfileDto Update(ProfileDto profiledDto)
        {
            var profileUpdate = _profileUpdateFactory.CreateItem(profiledDto);
            var updatedProfile = _profileService.Update(profileUpdate);
            return _profileDtoFactory.CreateItem(updatedProfile);
        }

        public ProfileDto GetMyProfile()
        {
            var profile = _profileRepository.GetForAccount(_userContext.Account.Id);
            return _profileDtoFactory.CreateItem(profile);
        }
    }
}