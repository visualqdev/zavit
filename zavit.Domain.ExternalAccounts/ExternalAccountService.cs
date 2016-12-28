using zavit.Domain.ExternalAccounts.Registrations;
using zavit.Domain.Profiles;

namespace zavit.Domain.ExternalAccounts
{
    public class ExternalAccountService : IExternalAccountService
    {
        readonly IProfileService _profileService;
        readonly INewExternalAccountProvider _newExternalAccountProvider;
        readonly IExternalAccountsRepository _externalAccountsRepository;

        public ExternalAccountService(IProfileService profileService, INewExternalAccountProvider newExternalAccountProvider, IExternalAccountsRepository externalAccountsRepository)
        {
            _profileService = profileService;
            _newExternalAccountProvider = newExternalAccountProvider;
            _externalAccountsRepository = externalAccountsRepository;
        }

        public ExternalAccount CreateExternalAccount(ExternalAccountRegistration externalAccountRegistration)
        {
            var accountRegistrationResult = _profileService.Register(externalAccountRegistration);

            var externalAccount = _newExternalAccountProvider.Provide(accountRegistrationResult.Account, externalAccountRegistration.Provider, externalAccountRegistration.Username);
            _externalAccountsRepository.Save(externalAccount);
            return externalAccount;
        }
    }
}