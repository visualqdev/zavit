namespace zavit.Domain.Messaging.Messages
{
    public class MessageService : IMessageService
    {
        readonly INewMessageProvider _newMessageProvider;
        readonly IMessageRepository _messageRepository;

        public MessageService(INewMessageProvider newMessageProvider, IMessageRepository messageRepository)
        {
            _newMessageProvider = newMessageProvider;
            _messageRepository = messageRepository;
        }

        public Message SendMessageOnThread(NewMessageRequest newMessageRequest, MessageThread messageThread)
        {
            var message = _newMessageProvider.Provide(newMessageRequest, messageThread);
            _messageRepository.Save(message);

            return message;
        }
    }
}