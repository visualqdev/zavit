using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.Dtos.Messaging.MessageThreads
{
    public class NewMessageThreadDto
    {
        public MessageThreadDto Thread { get; set; }
        public MessageDto Message { get; set; }
    }
}