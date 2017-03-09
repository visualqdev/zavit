using System.Threading.Tasks;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts
{
    public interface IExternalAccountService
    {
        Task<ExternalAccount> CreateExternalAccount(ExternalAccountRegistration externalAccountRegistration);
    }
}