using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageRepository
    {
        void Save(Message message);
        IResultCollection<MessageInfo> GetMessages(int messageThreadId, int? olderThanMessageId, int take);
    }
}