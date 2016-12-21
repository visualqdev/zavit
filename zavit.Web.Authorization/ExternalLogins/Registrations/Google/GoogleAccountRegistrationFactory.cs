using System.Threading.Tasks;
using zavit.Domain.ExternalAccounts.Registrations;
using zavit.Web.Authorization.ExternalLogins.Clients.Google;

namespace zavit.Web.Authorization.ExternalLogins.Registrations.Google
{
    public class GoogleAccountRegistrationFactory : IExternalAccountRegistrationFactory
    {
        readonly IGoogleLoginClient _googleLoginClient;

        public GoogleAccountRegistrationFactory(IGoogleLoginClient googleLoginClient)
        {
            _googleLoginClient = googleLoginClient;
        }

        public async Task<ExternalAccountRegistration> CreateRegistration(string provider, string accessToken)
        {
            var userInfo = await _googleLoginClient.GetUserInfo(accessToken);

            var externalAccountRegistration = new ExternalAccountRegistration
            {
                Provider = provider,
                DisplayName = userInfo.name,
                Email = userInfo.email,
                Username = userInfo.id
            };

            return externalAccountRegistration;
        }

        public bool CanCreate(string provider)
        {
            return provider == ExternalLoginProvider.Google;
        }
    }
}