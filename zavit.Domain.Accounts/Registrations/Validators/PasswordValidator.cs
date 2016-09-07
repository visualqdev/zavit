namespace zavit.Domain.Accounts.Registrations.Validators
{
    public class PasswordValidator : IAccountRegistrationValidator
    {
        public const string PassworddTooShort = "Please enter a Password longer than 6 character";

        readonly IAccountRegistrationResultFactory _accountRegistrationResultFactory;

        public PasswordValidator(IAccountRegistrationResultFactory accountRegistrationResultFactory)
        {
            _accountRegistrationResultFactory = accountRegistrationResultFactory;
        }

        public AccountRegistrationResult Validate(IAccountRegistration accountRegistration)
        {
            if (accountRegistration.Password != null && accountRegistration.Password.Length > 5) return null;

            return _accountRegistrationResultFactory.CreateFailed(PassworddTooShort);
        }
    }
}