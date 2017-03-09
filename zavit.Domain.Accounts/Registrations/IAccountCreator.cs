using System.Threading.Tasks;

namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountCreator
    {
        Task<Account> Create(IAccountRegistration accountRegistration);
    }
}