using System;
using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadObserver
    {
        void MessagesRead(IList<Message> completelyReadMessages, int messageThreadId);
    }
}