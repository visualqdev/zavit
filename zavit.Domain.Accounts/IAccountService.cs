using System.Threading.Tasks;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts
{
    public interface IAccountService
    {
        Task<AccountRegistrationResult> Register(IAccountRegistration accountRegistration);
    }
}