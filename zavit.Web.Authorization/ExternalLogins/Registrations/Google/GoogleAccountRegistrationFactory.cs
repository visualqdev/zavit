using System;
using System.Threading.Tasks;
using zavit.Domain.ExternalAccounts.Registrations;
using zavit.Domain.Profiles;
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

            Gender gender;
            if (!Enum.TryParse(userInfo.gender, out gender))
                gender = Gender.NotSpecified;

            var externalAccountRegistration = new ExternalAccountRegistration
            {
                Provider = provider,
                DisplayName = userInfo.name,
                Email = userInfo.email,
                Username = userInfo.id,
                Gender = gender
            };

            return externalAccountRegistration;
        }

        public bool CanCreate(string provider)
        {
            return provider == ExternalLoginProvider.Google;
        }
    }
}