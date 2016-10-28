using System.Collections.Generic;
using zavit.Domain.Messaging.Messages;

namespace zavit.Domain.Messaging.MessageThreads
{
    public class MessageInbox : IMessageInbox
    {
        public IEnumerable<MessageThread> Threads { get; set; }
        public Dictionary<int, int> UnreadMessageCountsPerThread { get; set; }
        public Dictionary<int, Message> LatestMessagesPerThread { get; set; }
        public int AccountId { get; set; }

        public int UnreadMessageCount(int messageThreadId)
        {
            int count;
            return UnreadMessageCountsPerThread.TryGetValue(messageThreadId, out count) ? count : 0;
        }

        public Message GetLatestMessage(int messageThreadId)
        {
            Message message;
            return LatestMessagesPerThread.TryGetValue(messageThreadId, out message) ? message : null;
        }
    }
}