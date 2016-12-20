using System.Threading.Tasks;
using zavit.Web.Api.Dtos.ExternalAccounts;

namespace zavit.Web.Authorization.ExternalLogins.ExternalTokenVerifiers
{
    public interface IExternalAccessTokenVerifier
    {
        Task<ParsedExternalAccessToken> Verify(string accessToken);
        bool CanVerify(string provider);
    }
}