using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts
{
    public interface IAccountService
    {
        AccountRegistrationResult Register(IAccountRegistration accountRegistration);
    }
}