using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageInfo
    {
        public Message Message { get; set; } 
        public bool HasBeenRead { get; set; }
    }
}