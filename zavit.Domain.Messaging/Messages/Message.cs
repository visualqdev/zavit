﻿using System;
using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.Messaging.Messages
{
    public class Message : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Body { get; set; }
        public virtual MessageThread MessageThread { get; set; }
        public virtual Account Sender { get; set; }
        public virtual DateTime SentOn { get; set; }
    }
}