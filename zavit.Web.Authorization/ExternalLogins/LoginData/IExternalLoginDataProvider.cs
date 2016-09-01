using System.Security.Claims;

namespace zavit.Web.Authorization.ExternalLogins.LoginData
{
    public interface IExternalLoginDataProvider
    {
        ExternalLoginData Provide(ClaimsIdentity claimsIdentity);
    }
}