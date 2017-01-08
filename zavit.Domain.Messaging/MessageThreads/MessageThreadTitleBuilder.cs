using System.Linq;
using zavit.Domain.Accounts;

namespace zavit.Domain.Messaging.MessageThreads
{
    public class MessageThreadTitleBuilder : IMessageThreadTitleBuilder
    {
        public string BuildTitle(MessageThread messageThread, int requestedByAccountId)
        {
            return string.Join(", ", messageThread.Participants
                .Where(a => a.Id != requestedByAccountId)
                .Select(a => a.Profile.DisplayName));
        }
    }
}