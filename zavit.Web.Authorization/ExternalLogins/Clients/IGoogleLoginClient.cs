using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients
{
    public interface IGoogleLoginClient
    {
        Task<JObject> GetTokenInfo(string accessToken);
    }
}