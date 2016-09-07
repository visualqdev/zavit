namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistrationResultFactory
    {
        AccountRegistrationResult CreateSuccessful(Account account);
        AccountRegistrationResult CreateFailed(string message);
    }
}