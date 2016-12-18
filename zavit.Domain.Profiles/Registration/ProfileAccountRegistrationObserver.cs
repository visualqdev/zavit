using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Profiles.Registration
{
    public class ProfileAccountRegistrationObserver : IAccountRegistrationObserver
    {
        readonly IProfileRepository _profileRepository;
        readonly IProfileCreator _profileCreator;

        public ProfileAccountRegistrationObserver(IProfileRepository profileRepository, IProfileCreator profileCreator)
        {
            _profileRepository = profileRepository;
            _profileCreator = profileCreator;
        }

        public void AccountRegsitered(Account account)
        {
            var profile = _profileCreator.CreateProfile(account);
            _profileRepository.Save(profile);
        }
    }
}