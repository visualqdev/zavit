using System;
using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging
{
    public class MessageThread : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual IList<Account> Participants { get; set; }
        public virtual DateTime CreatedOn { get; set; }
    }
}