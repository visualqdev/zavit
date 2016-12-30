using System;
using System.Threading.Tasks;
using zavit.Domain.ExternalAccounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Web.Authorization.ExternalLogins.Clients.Facebook;

namespace zavit.Web.Authorization.ExternalLogins.Registrations.Facebook
{
    public class FacebookAccountRegistrationFactory : IExternalAccountRegistrationFactory
    {
        readonly IFacebookLoginClient _facebookLoginClient;
        
        public FacebookAccountRegistrationFactory(IFacebookLoginClient facebookLoginClient)
        {
            _facebookLoginClient = facebookLoginClient;
        }

        public async Task<ExternalAccountRegistration> CreateRegistration(string provider, string accessToken)
        {
            var userInfo = await _facebookLoginClient.GetUserInfo(accessToken);

            Gender gender;
            if (!Enum.TryParse(userInfo.gender, true, out gender))
                gender = Gender.NotSpecified;

            var externalAccountRegistration = new ExternalAccountRegistration
            {
                Provider = provider,
                DisplayName = userInfo.name,
                Email = userInfo.email,
                Username = userInfo.id,
                Gender = gender
            };

            var image = await _facebookLoginClient.GetProfileImage(userInfo.id);
            externalAccountRegistration.ProfileImage = image;

            return externalAccountRegistration;
        }

        public bool CanCreate(string provider)
        {
            return provider == ExternalLoginProvider.Facebook;
        }
    }
}