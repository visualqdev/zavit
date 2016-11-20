using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;

namespace zavit.Web.Mvc.SignalR.Messaging.Observers
{
    public class MessageSentSignalRObserver : IMessageSentObserver
    {
        readonly IThreadMessageBroadcastRequestFactory _messageBroadcastRequestFactory;
        readonly IMessagingBroadcaster _messagingBroadcaster;

        public MessageSentSignalRObserver(IThreadMessageBroadcastRequestFactory messageBroadcastRequestFactory, IMessagingBroadcaster messagingBroadcaster)
        {
            _messageBroadcastRequestFactory = messageBroadcastRequestFactory;
            _messagingBroadcaster = messagingBroadcaster;
        }

        public void MessageSent(Message message)
        {
            var messageBroadcastRequest =_messageBroadcastRequestFactory.CreateItem(message);
            _messagingBroadcaster.ThreadMessageSent(messageBroadcastRequest);
        }
    }
}