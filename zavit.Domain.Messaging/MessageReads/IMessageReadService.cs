using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadService
    {
        void MessagesRead(int messageThreadId, Account account);
    }
}