using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.SignalR.Hubs
{
    [HubName("messagingHub")]
    public class MessagingHub : Hub
    {
        public Task JoinInboxNotifications(string accountId)
        {
            return Groups.Add(Context.ConnectionId, InboxGroupIdProvider.Provide(accountId));
        }

        public Task LeaveInboxNotifications(string accountId)
        {
            return Groups.Remove(Context.ConnectionId, InboxGroupIdProvider.Provide(accountId));
        }

        public Task JoinThreadNotifications(string accountId, string messageThreadId)
        {
            return Groups.Add(Context.ConnectionId, ThreadGroupIdProvider.Provide(messageThreadId, accountId));
        }

        public Task LeaveThreadNotifications(string accountId, string messageThreadId)
        {
            return Groups.Remove(Context.ConnectionId, ThreadGroupIdProvider.Provide(messageThreadId, accountId));
        }
    }
}