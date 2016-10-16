namespace zavit.Domain.Messaging.Messages
{
    public interface INewMessageProvider
    {
        Message Provide(NewMessageRequest newMessageRequest, MessageThread messageThread);
    }
}