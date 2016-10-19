using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageRepository
    {
        void Save(Message message);
        IResultCollection<Message> GetMessages(int messageThreadId, int? olderThanMessageId, int take);
    }
}