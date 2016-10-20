using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageService
    {
        Message SendMessageOnThread(NewMessageRequest newMessageRequest, MessageThread messageThread);
        Message SendMessageOnThread(NewMessageRequest newMessageRequest, int messageThreadId);
        IResultCollection<MessageInfo> GetMessages(int messageThreadId, int? olderThanMessageId, int take, Account account);
    }
}