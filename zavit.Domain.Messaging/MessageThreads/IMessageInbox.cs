using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageInbox
    {
        IEnumerable<MessageThread> Threads { get; }
        int AccountId { get; }
        int UnreadMessageCount(int messageThreadId);
        Message GetLatestMessage(int messageThreadId);
    }
}