using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageReadCreator : IMessageReadCreator
    {
        public MessageRead Create(Account account, Message message)
        {
            return new MessageRead
            {
                Message = message,
                Account = account
            };
        }
    }
}