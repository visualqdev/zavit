using System;
using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.Messages
{
    public class NewMessageRequest
    {
        public string Body { get; set; }
        public Account Sender { get; set; }
        public Guid Stamp { get; set; }
    }
}