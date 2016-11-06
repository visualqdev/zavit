using System.Web;

namespace zavit.Web.Mvc.SignalR.ConnectionIds
{
    public class ConnectionIdProvider : IConnectionIdProvider
    {
        const string ConnectionName = "connectionid";

        public string GetConnectionId()
        {
            var connectionId = GetHeaderValue(ConnectionName);
            return connectionId;
        }

        static string GetHeaderValue(string key)
        {
            var request = HttpContext.Current.Request;
            return request.Headers[$"X-{key}"];
        }
    }
}