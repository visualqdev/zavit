namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistrationResultFactory
    {
        AccountRegistrationResult CreateSuccessful();
        AccountRegistrationResult CreateFailed(string message);
    }
}