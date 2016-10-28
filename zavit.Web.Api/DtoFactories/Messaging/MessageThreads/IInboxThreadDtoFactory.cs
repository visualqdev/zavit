using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageThreads;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public interface IInboxThreadDtoFactory
    {
        InboxThreadDto CreateItem(MessageThread messageThread, IMessageInbox messageInbox);
    }
}