using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.GroupIds;

namespace zavit.Web.Mvc.SignalR.Hubs
{
    public class MessagingHub : Hub
    {
        public const string InboxNotificationPrefix = "messageinbox_";
        public const string ThreadNotificationPrefix = "messagethread_";

        public Task JoinInboxNotifications(string accountId)
        {
            return Groups.Add(Context.ConnectionId, $"{InboxNotificationPrefix}{accountId}");
        }

        public Task LeaveInboxNotifications(string accountId)
        {
            return Groups.Remove(Context.ConnectionId, $"{InboxNotificationPrefix}{accountId}");
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