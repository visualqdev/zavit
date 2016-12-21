using System.Threading.Tasks;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Web.Authorization.ExternalLogins.Registrations
{
    public interface IExternalAccountRegistrationFactory
    {
        Task<ExternalAccountRegistration> CreateRegistration(string provider, string accessToken);
        bool CanCreate(string provider);
    }
}