using System;

namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageThreadRepository
    {
        void Save(MessageThread messageThread);
        bool CanUserAccessThread(int accountId, int? messageThreadId);
        MessageThread GetMessageThread(int messageThreadId);
        IMessageInbox GetInbox(int accountId);
        int GetMessageThreadIdByMessage(Guid messageStamp);
    }
}