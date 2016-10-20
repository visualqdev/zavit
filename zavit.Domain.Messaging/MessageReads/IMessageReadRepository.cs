using System;
using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadRepository
    {
        IList<Message> UnreadMessagesByUser(int messageThreadId, int accountId, DateTime dateRead);
        IList<int> GetReadMessageIds(int messageThreadId, IEnumerable<int> messageIds);
        void Save(IEnumerable<MessageRead> instantMessageReads);
    }
}