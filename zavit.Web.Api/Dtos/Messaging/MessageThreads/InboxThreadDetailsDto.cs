using System.Collections.Generic;
using zavit.Web.Api.Dtos.Messaging.Messages;

namespace zavit.Web.Api.Dtos.Messaging.MessageThreads
{
    public class InboxThreadDetailsDto
    {
        public string ThreadTitle { get; set; }
        public int ThreadId { get; set; }
        public MessagesCollectionDto MessagesCollection { get; set; }
    }
}