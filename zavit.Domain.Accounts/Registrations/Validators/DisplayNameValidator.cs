namespace zavit.Domain.Accounts.Registrations.Validators
{
    public class DisplayNameValidator : IAccountRegistrationValidator
    {
        readonly IAccountRegistrationResultFactory _accountRegistrationResultFactory;
        public const string DisplayNameBlankMessage = "Please enter a Display name";

        public DisplayNameValidator(IAccountRegistrationResultFactory accountRegistrationResultFactory)
        {
            _accountRegistrationResultFactory = accountRegistrationResultFactory;
        }

        public AccountRegistrationResult Validate(IAccountRegistration accountRegistration)
        {
            if (!string.IsNullOrWhiteSpace(accountRegistration.DisplayName)) return null;

            return _accountRegistrationResultFactory.CreateFailed(DisplayNameBlankMessage);
        }
    }
}