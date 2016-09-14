using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace zavit.Web.Authorization.ExternalLogins.LoginData
{
    public class ExternalLoginDataProvider : IExternalLoginDataProvider
    {
        public ExternalLoginData Provide(ClaimsIdentity claimsIdentity)
        {
            var providerKeyClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

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
                UserName = claimsIdentity.FindFirstValue(ClaimTypes.Name),
                UserEmail = claimsIdentity.FindFirstValue(ClaimTypes.Email),
                ExternalAccessToken = claimsIdentity.FindFirstValue("ExternalAccessToken"),
            };
        }
    }
}