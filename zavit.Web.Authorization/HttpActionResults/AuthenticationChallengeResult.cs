using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace zavit.Web.Authorization.HttpActionResults
{
    public class AuthenticationChallengeResult : IHttpActionResult
    {
        public string LoginProvider { get; set; }
        public HttpRequestMessage Request { get; set; }

        public AuthenticationChallengeResult(string loginProvider, ApiController controller)
        {
            LoginProvider = loginProvider;
            Request = controller.Request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request
            };
            return Task.FromResult(response);
        }
    }
}