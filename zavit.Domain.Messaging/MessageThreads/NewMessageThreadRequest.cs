using System.Collections.Generic;

namespace zavit.Domain.Messaging.MessageThreads
{
    public class NewMessageThreadRequest
    {
        public IEnumerable<int> ParticipantIds { get; set; }
    }
}