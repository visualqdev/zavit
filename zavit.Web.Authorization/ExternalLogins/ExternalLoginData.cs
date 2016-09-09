using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace zavit.Web.Authorization.ExternalLogins
{
    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string ExternalAccessToken { get; set; }
        public string UserEmail { get; set; }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        {
            var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirstValue(ClaimTypes.Name),
                UserEmail = identity.FindFirstValue(ClaimTypes.Email),
                ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
            };
        }
    }
}