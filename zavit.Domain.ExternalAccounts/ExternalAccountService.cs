using zavit.Domain.Accounts;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts
{
    public class ExternalAccountService : IExternalAccountService
    {
        readonly IExternalAccountRegistrationFactory _externalAccountRegistrationFactory;
        readonly IAccountService _accountService;
        readonly INewExternalAccountProvider _newExternalAccountProvider;
        readonly IExternalAccountsRepository _externalAccountsRepository;

        public ExternalAccountService(IExternalAccountRegistrationFactory externalAccountRegistrationFactory, IAccountService accountService, INewExternalAccountProvider newExternalAccountProvider, IExternalAccountsRepository externalAccountsRepository)
        {
            _externalAccountRegistrationFactory = externalAccountRegistrationFactory;
            _accountService = accountService;
            _newExternalAccountProvider = newExternalAccountProvider;
            _externalAccountsRepository = externalAccountsRepository;
        }

        public ExternalAccount CreateExternalAccount(string loginProvider, string externalUserId, string displayName, string email)
        {
            var accountRegistration = _externalAccountRegistrationFactory.CreateItem(externalUserId, displayName, email);
            var accountRegistrationResult = _accountService.Register(accountRegistration);

            var externalAccount = _newExternalAccountProvider.Provide(accountRegistrationResult.Account, loginProvider, externalUserId);
            _externalAccountsRepository.Save(externalAccount);
            return externalAccount;
        }
    }
}