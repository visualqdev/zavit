namespace zavit.Domain.Accounts.Registrations
{
    public class AccountRegistrationResultFactory : IAccountRegistrationResultFactory
    {
        public AccountRegistrationResult CreateSuccessful(Account account)
        {
            return new AccountRegistrationResult
            {
                Success = true,
                Account = account
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