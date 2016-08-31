using System.Security.Claims;

namespace zavit.Web.Api.Authorization
{
    public class ClaimsIdentityProvider : IClaimsIdentityProvider
    {
        ClaimsIdentity _claimsIdentity;

        public void SetIdentity(ClaimsIdentity claimsIdentity)
        {
            _claimsIdentity = claimsIdentity;
        }

        public string Username => _claimsIdentity?.FindFirst(ClaimTypes.Name)?.Value;
    }
}