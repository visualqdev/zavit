using System;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadService
    {
        void MessagesRead(int messageThreadId, Account account);
        void MessageSent(Message message);
    }
}