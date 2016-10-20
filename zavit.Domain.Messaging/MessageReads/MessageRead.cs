using System;
using zavit.Domain.Accounts;
using zavit.Domain.Messaging.Messages;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageRead : IEntity<int>
    {
        public virtual Message Message { get; set; }
        public virtual Account Account { get; set; }
        public virtual DateTime DateRead { get; set; }
        public virtual int Id { get; set; }
    }
}