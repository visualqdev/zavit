using System.Collections.Generic;

namespace zavit.Web.Api.Dtos.Messaging.MessageThreads
{
    public class MessageThreadDto
    {
        public int ThreadId { get; set; }
        public IEnumerable<ThreadParticipantDto> Participants { get; set; }
    }
}