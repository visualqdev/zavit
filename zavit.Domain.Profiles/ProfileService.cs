using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public class ProfileService : IProfileService
    {
        readonly IProfileRepository _profileRepository;
        readonly IEnumerable<IProfileUpdater> _profileUpdaters;
        readonly IAccountService _accountService;
        readonly IProfileCreator _profileCreator;

        public ProfileService(IProfileRepository profileRepository, IEnumerable<IProfileUpdater> profileUpdaters, IAccountService accountService, IProfileCreator profileCreator)
        {
            _profileRepository = profileRepository;
            _profileUpdaters = profileUpdaters;
            _accountService = accountService;
            _profileCreator = profileCreator;
        }

        public Profile Update(ProfileUpdate profileUpdate)
        {
            var profile = _profileRepository.GetForAccount(profileUpdate.Account.Id);
            if (profile.AcceptUpdate(profileUpdate, _profileUpdaters))
            {
                _profileRepository.Update(profile);
            }

            return profile;
        }

        public AccountRegistrationResult Register(IAccountProfileRegistration accountProfileRegistration)
        {
            var accountRegistrationResult = _accountService.Register(accountProfileRegistration);

            if (accountRegistrationResult.Success)
            {
                var profile = _profileCreator.CreateProfile(accountRegistrationResult.Account, accountProfileRegistration);
                _profileRepository.Save(profile);
            }

            return accountRegistrationResult;
        }
    }
}