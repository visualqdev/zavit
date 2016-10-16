namespace zavit.Domain.Messaging.Messages
{
    public interface IMessageService
    {
        Message SendMessageOnThread(NewMessageRequest newMessageRequest, MessageThread messageThread);
    }
}