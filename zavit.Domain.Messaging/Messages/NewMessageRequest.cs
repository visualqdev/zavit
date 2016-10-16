using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.Messages
{
    public class NewMessageRequest
    {
        public string Body { get; set; }
        public Account Sender { get; set; }
    }
}