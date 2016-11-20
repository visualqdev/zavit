using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageThreadService
    {
        MessageThread CreateNewThread(NewMessageThreadRequest newMessageThreadRequest);
        MessageThread GetMessageThread(int messageThreadId);
        IMessageInbox GetMessageInbox(Account account);
    }
}