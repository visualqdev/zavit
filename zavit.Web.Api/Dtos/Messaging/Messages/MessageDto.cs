using System;

namespace zavit.Web.Api.Dtos.Messaging.Messages
{
    public class MessageDto
    {
        public string Body { get; set; }
        public DateTime SentOn { get; set; }
        public int Id { get; set; }
    }
}