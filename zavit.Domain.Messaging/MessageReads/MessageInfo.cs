using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageReads
{
    public class MessageInfo
    {
        public Message Message { get; set; } 
        public MessageStatus Status { get; set; }
    }
}