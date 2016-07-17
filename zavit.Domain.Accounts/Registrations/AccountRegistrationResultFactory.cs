namespace zavit.Domain.Accounts.Registrations
{
    public class AccountRegistrationResultFactory : IAccountRegistrationResultFactory
    {
        public AccountRegistrationResult CreateSuccessful()
        {
            return new AccountRegistrationResult
            {
                Success = true
            };
        }

        public AccountRegistrationResult CreateFailed(string message)
        {
            return new AccountRegistrationResult
            {
                Success = false,
                ErrorMessage = message
            };
        }
    }
}