using zavit.Domain.Messaging;
using zavit.Web.Api.Dtos.Messaging.MessageThreads;

namespace zavit.Web.Api.DtoFactories.Messaging.MessageThreads
{
    public interface IMessageThreadDtoFactory
    {
        MessageThreadDto CreateItem(MessageThread messageThread);
    }
}