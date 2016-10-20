using System;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageRead
    {
        public virtual Message Message { get; set; }
        public virtual Account Account { get; set; }
        public virtual DateTime DateRead { get; set; }
    }
}