using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageThreadService
    {
        MessageThread CreateNewThread(NewMessageThreadRequest newMessageThreadRequest);
        MessageThread GetMessageThread(int messageThreadId);
        IMessageInbox GetMessageInbox(Account account);
    }
}