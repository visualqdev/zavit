using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public class InboxThreadDtoFactory : IInboxThreadDtoFactory
    {
        readonly IMessageThreadTitleBuilder _messageThreadTitleBuilder;

        public InboxThreadDtoFactory(IMessageThreadTitleBuilder messageThreadTitleBuilder)
        {
            _messageThreadTitleBuilder = messageThreadTitleBuilder;
        }

        public InboxThreadDto CreateItem(MessageThread messageThread, IMessageInbox messageInbox)
        {
            var latestMessage = messageInbox.GetLatestMessage(messageThread.Id);

            var inboxThreadDto = new InboxThreadDto
            {
                ThreadTitle = _messageThreadTitleBuilder.BuildTitle(messageThread, messageInbox.AccountId),
                ThreadId = messageThread.Id,
                UnreadMessageCount = messageInbox.UnreadMessageCount(messageThread.Id)
            };

            if (latestMessage != null)
            {
                inboxThreadDto.LatestMessageBody = latestMessage.Body;
                inboxThreadDto.LatestMessageSentOn = latestMessage.SentOn;
            }

            return inboxThreadDto;
        }
    }
}