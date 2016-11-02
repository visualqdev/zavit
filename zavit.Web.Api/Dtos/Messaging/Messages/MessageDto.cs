using System;
using zavit.Domain.Messaging.MessageReads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.Dtos.Messaging.Messages
{
    public class MessageDto
    {
        public string Body { get; set; }
        public DateTime SentOn { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public ThreadParticipantDto Sender { get; set; }
        public Guid Stamp { get; set; }
    }
}