using zavit.Domain.Messaging;
using zavit.Domain.Messaging.MessageReads;
using zavit.Domain.Shared.ResultCollections;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public interface IInboxThreadDetailsDtoFactory
    {
        InboxThreadDetailsDto CreateItem(MessageThread messageThread, IResultCollection<MessageInfo> messageResultsCollection);
    }
}