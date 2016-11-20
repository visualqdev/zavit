using System;

namespace zavit.Web.Api.Dtos.Messaging.MessageThreads
{
    public class InboxThreadDto
    {
        public int ThreadId { get; set; }
        public string ThreadTitle { get; set; }
        public int UnreadMessageCount { get; set; }
        public string LatestMessageBody { get; set; }
        public DateTime LatestMessageSentOn { get; set; }
    }
}