using System.Security.Claims;

namespace zavit.Web.Api.Authorization
{
    public interface IClaimsIdentityProvider
    {
        void SetIdentity(ClaimsIdentity claimsIdentity);
        string Username { get; }
    }
}