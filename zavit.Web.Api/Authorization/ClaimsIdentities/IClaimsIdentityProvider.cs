using System.Security.Claims;

namespace zavit.Web.Api.Authorization.ClaimsIdentities
{
    public interface IClaimsIdentityProvider
    {
        void SetIdentity(ClaimsIdentity claimsIdentity);
        string Username { get; }
    }
}