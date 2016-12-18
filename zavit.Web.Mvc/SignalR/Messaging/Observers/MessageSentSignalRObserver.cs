using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;

namespace zavit.Web.Mvc.SignalR.Messaging.Observers
{
    public class MessageSentSignalRObserver : IMessageSentObserver
    {
        readonly IThreadMessageBroadcastRequestFactory _messageBroadcastRequestFactory;
        readonly IInboxMessageBroadcastRequestFactory _inboxMessageBroadcastRequestFactory;
        readonly IMessagingBroadcaster _messagingBroadcaster;

        public MessageSentSignalRObserver(IThreadMessageBroadcastRequestFactory messageBroadcastRequestFactory, IMessagingBroadcaster messagingBroadcaster, IInboxMessageBroadcastRequestFactory inboxMessageBroadcastRequestFactory)
        {
            _messageBroadcastRequestFactory = messageBroadcastRequestFactory;
            _messagingBroadcaster = messagingBroadcaster;
            _inboxMessageBroadcastRequestFactory = inboxMessageBroadcastRequestFactory;
        }

        public void MessageSent(Message message)
        {
            var threadMessageBroadcastRequest =_messageBroadcastRequestFactory.CreateItem(message);
            _messagingBroadcaster.ThreadMessageSent(threadMessageBroadcastRequest);

            var inboxMessageBroadcastRequest = _inboxMessageBroadcastRequestFactory.CreateItem(message);
            _messagingBroadcaster.InboxMessageSent(inboxMessageBroadcastRequest);
        }
    }
}