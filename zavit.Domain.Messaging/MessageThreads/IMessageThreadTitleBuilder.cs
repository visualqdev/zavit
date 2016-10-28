using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.MessageThreads
{
    public interface IMessageThreadTitleBuilder
    {
        string BuildTitle(MessageThread messageThread, int requestedByAccountId);
    }
}