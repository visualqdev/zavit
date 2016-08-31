namespace zavit.Domain.Accounts.Registrations.Validators
{
    public interface IAccountRegistrationValidator
    {
        AccountRegistrationResult Validate(IAccountRegistration accountRegistration);
    }
}