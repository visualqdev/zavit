using System;
using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public interface IMessageReadProcessor
    {
        void Process(IList<Message> unreadMessages, Account account, DateTime dateRead);
    }
}