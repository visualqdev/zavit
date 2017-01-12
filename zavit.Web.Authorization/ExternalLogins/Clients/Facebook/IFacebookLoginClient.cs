using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients.Facebook
{
    public interface IFacebookLoginClient
    {
        Task<JObject> GetTokenInfo(string accessToken);
        Task<FacebookUserInfoDto> GetUserInfo(string accessToken);
        Task<byte[]> GetProfileImage(string userId);
    }
}