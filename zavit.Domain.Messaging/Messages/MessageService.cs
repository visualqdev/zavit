using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Messages
{
    public class MessageService : IMessageService
    {
        readonly INewMessageProvider _newMessageProvider;
        readonly IMessageRepository _messageRepository;
        readonly IMessageThreadRepository _messageThreadRepository;

        public MessageService(INewMessageProvider newMessageProvider, IMessageRepository messageRepository, IMessageThreadRepository messageThreadRepository)
        {
            _newMessageProvider = newMessageProvider;
            _messageRepository = messageRepository;
            _messageThreadRepository = messageThreadRepository;
        }

        public Message SendMessageOnThread(NewMessageRequest newMessageRequest, MessageThread messageThread)
        {
            var message = _newMessageProvider.Provide(newMessageRequest, messageThread);
            _messageRepository.Save(message);

            return message;
        }

        public Message SendMessageOnThread(NewMessageRequest newMessageRequest, int messageThreadId)
        {
            var messageThread = _messageThreadRepository.GetMessageThread(messageThreadId);
            return SendMessageOnThread(newMessageRequest, messageThread);
        }

        public IResultCollection<Message> GetMessages(int messageThreadId, int? olderThanMessageId, int take, Account account)
        {
            var messageThreadCollection = _messageRepository.GetMessages(messageThreadId, olderThanMessageId, take);
            return messageThreadCollection;
        }
    }
}