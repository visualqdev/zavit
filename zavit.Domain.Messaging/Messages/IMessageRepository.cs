namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageRepository
    {
        void Save(Message message);
    }
}