using System;
using System.Collections.Generic;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.Messages;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting;
using zavit.Web.Mvc.SignalR.Messaging.Broadcasting.DtoFactories;

namespace zavit.Web.Mvc.SignalR.Messaging.Observers
{
    public class MessageReadSignalRObserver : IMessageReadObserver
    {
        readonly IMessagingBroadcaster _messagingBroadcaster;
        readonly IReadMessagesBroadcastRequestsProvider _readMessagesBroadcastRequestsProvider;

        public MessageReadSignalRObserver(IMessagingBroadcaster messagingBroadcaster, IReadMessagesBroadcastRequestsProvider readMessagesBroadcastRequestsProvider)
        {
            _messagingBroadcaster = messagingBroadcaster;
            _readMessagesBroadcastRequestsProvider = readMessagesBroadcastRequestsProvider;
        }

        public void MessagesRead(IList<Message> completelyReadMessages, int messageThreadId)
        {
            var messagesReadBroadcastRequests = _readMessagesBroadcastRequestsProvider.Provide(completelyReadMessages, messageThreadId);

            foreach (var messagesReadBroadcastRequest in messagesReadBroadcastRequests)
            {
                _messagingBroadcaster.ThreadMessageRead(messagesReadBroadcastRequest);
            }
        }
    }
}