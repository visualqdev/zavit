using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadCreator
    {
        MessageRead Create(Account account, Message message);
    }
}