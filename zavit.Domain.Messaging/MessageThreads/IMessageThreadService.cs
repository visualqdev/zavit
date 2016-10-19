namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageThreadService
    {
        MessageThread CreateNewThread(NewMessageThreadRequest newMessageThreadRequest);
        MessageThread GetMessageThread(int messageThreadId);
    }
}