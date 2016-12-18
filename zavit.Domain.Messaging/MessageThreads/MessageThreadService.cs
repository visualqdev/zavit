using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.MessageThreads
{
    public class MessageThreadService : IMessageThreadService
    {
        readonly INewMessageThreadProvider _messageThreadProvider;
        readonly IMessageThreadRepository _messageThreadRepository;

        public MessageThreadService(INewMessageThreadProvider messageThreadProvider, IMessageThreadRepository messageThreadRepository)
        {
            _messageThreadProvider = messageThreadProvider;
            _messageThreadRepository = messageThreadRepository;
        }

        public MessageThread CreateNewThread(NewMessageThreadRequest newMessageThreadRequest)
        {
            var messageThread = _messageThreadProvider.Provide(newMessageThreadRequest);
            _messageThreadRepository.Save(messageThread);

            return messageThread;
        }

        public MessageThread GetMessageThread(int messageThreadId)
        {
            return _messageThreadRepository.GetMessageThread(messageThreadId);
        }

        public IMessageInbox GetMessageInbox(Account account)
        {
            return _messageThreadRepository.GetInbox(account.Id);
        }
    }
}