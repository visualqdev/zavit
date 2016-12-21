using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using zavit.Web.Authorization.ExternalLogins.Registrations.Google;

namespace zavit.Web.Authorization.ExternalLogins.Clients.Google
{
    public interface IGoogleLoginClient
    {
        Task<JObject> GetTokenInfo(string accessToken);
        Task<GoogleUserInfoDto> GetUserInfo(string accessToken);
    }
}