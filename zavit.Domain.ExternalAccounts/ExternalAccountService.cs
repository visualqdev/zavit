using zavit.Domain.Accounts;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts
{
    public class ExternalAccountService : IExternalAccountService
    {
        readonly IAccountService _accountService;
        readonly INewExternalAccountProvider _newExternalAccountProvider;
        readonly IExternalAccountsRepository _externalAccountsRepository;

        public ExternalAccountService(IAccountService accountService, INewExternalAccountProvider newExternalAccountProvider, IExternalAccountsRepository externalAccountsRepository)
        {
            _accountService = accountService;
            _newExternalAccountProvider = newExternalAccountProvider;
            _externalAccountsRepository = externalAccountsRepository;
        }

        public ExternalAccount CreateExternalAccount(ExternalAccountRegistration externalAccountRegistration)
        {
            var accountRegistrationResult = _accountService.Register(externalAccountRegistration);

            var externalAccount = _newExternalAccountProvider.Provide(accountRegistrationResult.Account, externalAccountRegistration.Provider, externalAccountRegistration.Username);
            _externalAccountsRepository.Save(externalAccount);
            return externalAccount;
        }
    }
}