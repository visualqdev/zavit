using zavit.Domain.Accounts;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Messaging.Messages
{
    public class MessageService : IMessageService
    {
        readonly INewMessageProvider _newMessageProvider;
        readonly IMessageRepository _messageRepository;
        readonly IMessageThreadRepository _messageThreadRepository;
        readonly IMessageReadService _messageReadService;

        public MessageService(INewMessageProvider newMessageProvider, IMessageRepository messageRepository, IMessageThreadRepository messageThreadRepository, IMessageReadService messageReadService)
        {
            _newMessageProvider = newMessageProvider;
            _messageRepository = messageRepository;
            _messageThreadRepository = messageThreadRepository;
            _messageReadService = messageReadService;
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

        public IResultCollection<MessageInfo> GetMessages(int messageThreadId, int? olderThanMessageId, int take, Account account)
        {
            var messageThreadCollection = _messageRepository.GetMessages(messageThreadId, olderThanMessageId, take);

            _messageReadService.MessagesRead(messageThreadId, account);

            return messageThreadCollection;
        }
    }
}