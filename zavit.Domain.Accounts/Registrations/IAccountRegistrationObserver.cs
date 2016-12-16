namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistrationObserver
    {
        void AccountRegsitered(Account account);
    }
}