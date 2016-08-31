namespace zavit.Domain.Accounts.Registrations.Validators
{
    public class UsernameValidator : IAccountRegistrationValidator
    {
        readonly IAccountRepository _accountRepository;
        readonly IAccountRegistrationResultFactory _accountRegistrationResultFactory;
        public const string UsernameTakenMessage = "Username has already been used";

        public UsernameValidator(IAccountRepository accountRepository, IAccountRegistrationResultFactory accountRegistrationResultFactory)
        {
            _accountRepository = accountRepository;
            _accountRegistrationResultFactory = accountRegistrationResultFactory;
        }

        public AccountRegistrationResult Validate(IAccountRegistration accountRegistration)
        {
            if (!_accountRepository.AccountExists(accountRegistration.Username)) return null;

            return _accountRegistrationResultFactory.CreateFailed(UsernameTakenMessage);
        }
    }
}