namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageSentObserver
    {
        void MessageSent(Message message);
    }
}