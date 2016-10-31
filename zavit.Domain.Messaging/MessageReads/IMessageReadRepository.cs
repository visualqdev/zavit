using System.Collections.Generic;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadRepository
    {
        IList<int> GetReadMessageIds(IEnumerable<int> messageIds);
        void Save(IEnumerable<MessageRead> instantMessageReads);
        void Update(IEnumerable<MessageRead> messageReads);
        IList<MessageRead> GetPendingMessageReads(int messageThreadId, int accountId);
    }
}