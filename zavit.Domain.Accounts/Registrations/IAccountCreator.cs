namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountCreator
    {
        Account Create(IAccountRegistration accountRegistration);
    }
}