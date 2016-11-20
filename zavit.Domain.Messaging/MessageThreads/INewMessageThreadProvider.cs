namespace zavit.Domain.Messaging.MessageThreads
{
    public interface INewMessageThreadProvider
    {
        MessageThread Provide(NewMessageThreadRequest newMessageThreadRequest);
    }
}