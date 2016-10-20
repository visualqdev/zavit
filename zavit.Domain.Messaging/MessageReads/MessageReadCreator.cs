using System;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageReadCreator : IMessageReadCreator
    {
        public MessageRead Create(Account account, Message message, DateTime dateRead)
        {
            return new MessageRead
            {
                DateRead = dateRead,
                Message = message,
                Account = account
            };
        }
    }
}